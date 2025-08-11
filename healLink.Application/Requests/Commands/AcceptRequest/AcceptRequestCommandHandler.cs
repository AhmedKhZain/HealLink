using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.PatientDoctorSubscriptions;
using HealLink.Domain.Payments;
using HealLink.Domain.Requests;
using HealLink.Infrastructure.Services.Payment;
using MediatR;
using Stripe;

namespace healLink.Application.Requests.Commands.AcceptRequest;

public class AcceptRequestCommandHandler
    (IUnitOfWork unitOfWork,
    IDoctorRequestRepository doctorRequestRepository,
    ISubscriptionRepository subscriptionRepository,
    IPaymentRepository paymentRepository,
    IPaymentService paymentService,
    IRefundRepository refundRepository)
    : IRequestHandler<AcceptRequestCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AcceptRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await doctorRequestRepository.GetById(command.RequestId);
        if (request == null) 
            return Error.NotFound();
        if (request.Type == RequestType.Renewal 
            && !await subscriptionRepository.ExistsActiveByPatientIdAndDoctorIdAsync(request.SenderId, request.DoctorId))
            return Error.Conflict(description: "No Subscription Is available with the inserted data now.");
        if (request.Type == RequestType.Initial
            && await subscriptionRepository.ExistsActiveByPatientIdAndDoctorIdAsync(request.SenderId, request.DoctorId))
            return Error.Conflict(description: "There Is a Subscription Is available with the inserted data now, Make a renew Subscription instead");



        PatientDoctorSubscription subscription ;
        if (request.Type == RequestType.Renewal)
        {
            
            subscription = await subscriptionRepository.GetById((Guid)request.SubscriptionId);
            if(subscription == null) 
                return Error.NotFound();

            request.SetSubscription(subscription);
        }

        var payment = await paymentRepository.GetpaymentByRequestId(command.RequestId);
        if (payment is null)
            return Error.NotFound();


        ErrorOr<Success> result;
        PaymentIntent intent;
        try
        {
            intent = await paymentService.CapturePayment(payment.PaymentProviderId);
            payment.MarkAsCaptured();
        }
        catch
        {

            return Error.Conflict();
        }

        subscription = request.Accept();

        Func<Task> action;
        if (request.Type == RequestType.Initial)
        {
            action = async () =>
            {
                paymentRepository.UpdatePayment(payment);
                await subscriptionRepository.AddSubscriptionAsync(subscription); // Add not Update
                doctorRequestRepository.UpdateDoctorRequest(request);
            };
        }
        else
        {
            action = async () =>
            {
                paymentRepository.UpdatePayment(payment);
                subscriptionRepository.UpdateSubscription(subscription); // Update not Add
                doctorRequestRepository.UpdateDoctorRequest(request);

                await Task.CompletedTask;
            };
        }


        result = await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            await action.Invoke();
        });
        Refund refund;
        RefundItem item;

        if (result.IsError)
        {
            try
            {
                refund = await paymentService.RefundPayment(payment.PaymentProviderId);
                item = new RefundItem(payment.Id, refund.Id, payment.Amount);
                payment.MarkAsFailedToCancel();
            }
            catch
            {
                return Error.Failure(description: "Failure on refunding after failing in the subscription creating. ");
            }

            result = await unitOfWork.ExecuteInTransactionAsync(async () => {
                paymentRepository.UpdatePayment(payment);
                await refundRepository.AddRefundAsync(item);
            });
            if (result.IsError)
            {
                return Error.Failure(description:"Failure in updating data after refund after failing in the subscription creating. ");
            }


        }



        return result;

    }
}
