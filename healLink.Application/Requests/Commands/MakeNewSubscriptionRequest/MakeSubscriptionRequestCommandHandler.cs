using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Payments;
using HealLink.Domain.Requests;
using HealLink.Infrastructure.Services.Payment;
using MediatR;
using Stripe;

namespace healLink.Application.Requests.Commands.MakeNewSubscriptionRequest;

public class MakeSubscriptionRequestCommandHandler
    (IDoctorRequestRepository doctorRequestRepository,
    IUnitOfWork unitOfWork,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IPaymentRepository paymentRepository,
    IPaymentService paymentService,
    ISubscriptionRepository subscriptionRepository)
    : IRequestHandler<MakeSubscriptionRequestCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(MakeSubscriptionRequestCommand request, CancellationToken cancellationToken)
    {
        if (!await patientRepository.ExistsByUserIdAsync(request.PatientId)
            || !await doctorRepository.ExistsAsync(request.DoctorId))
        {
            return Error.NotFound( description:"not found doctor or patient ");
        }

        var plan = DoctorSubscriptionPlan.FromName(request.plan);
        var available = RequestType.TryFromName(request.RequestTypename, out var requestType);
        var IsThereSubscirption = await subscriptionRepository.ExistsActiveByPatientIdAndDoctorIdAsync(request.PatientId, request.DoctorId);

        if (requestType == RequestType.Renewal&& !IsThereSubscirption)
            return Error.Conflict(description: "No Subscription Is available with the inserted data now, Make A new Subscription instead.");

        if (requestType == RequestType.Initial&& IsThereSubscirption)
            return Error.Conflict(description: "There Is a Subscription Is available with the inserted data now, Make a renew Subscription instead.");



        PaymentIntent paymentIntent;
        try
        {
            paymentIntent = await paymentService.AuthorizePayment(request.PaymentId, plan);
        }
        catch (StripeException ex)
        {
            return Error.Failure(description: "Failed to authorize payment.\n\n\n\n"+ex.Message);
        }

        var RequestToAdd = new DoctorRequest(
            doctorId: request.DoctorId,
            senderId: request.PatientId,
            plan: plan,
            type: requestType?? RequestType.Initial,
            subscriptionId: request.subscriptionId,
            attachedFilePath: request.FileLink
        );

        var PaymentToAdd = new Payment(
            doctorRequestId: RequestToAdd.Id,
            amount: plan.Price,
            status: PaymentStatus.Authorized,
            paymentProviderId: paymentIntent.Id 
        );


        var result = await unitOfWork.ExecuteInTransactionAsync(
            async () => await doctorRequestRepository.AddDoctorRequestAsync(RequestToAdd),
            async () => await paymentRepository.AddPaymentAsync(PaymentToAdd)
            );

        if (result.IsError)
        {
            try
            {
                paymentIntent = await paymentService.CancelPayment(paymentIntent.Id);
                PaymentToAdd.MarkAsFailed();
            }
            catch (StripeException ex)
            {
                return Error.Failure(description: "Failed to cancel payment after request creation failure."+ex.Message);
            }


            return result.Errors;

        }

        return RequestToAdd.Id;
    }

}



