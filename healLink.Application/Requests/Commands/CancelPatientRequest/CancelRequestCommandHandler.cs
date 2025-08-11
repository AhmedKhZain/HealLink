using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using healLink.Application.Requests.Commands.CancelPatientRequest;
using HealLink.Domain.Payments;
using HealLink.Infrastructure.Services.Payment;
using MediatR;
using Stripe;

namespace healLink.Application.Requests.Commands.CancelPatientRequest;

public class CancelRequestCommandHandler 
    (IDoctorRequestRepository doctorRequestRepository,
    IUnitOfWork unitOfWork,
    IPaymentRepository paymentRepository,
    IPaymentService paymentService): IRequestHandler<CancelRequestCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CancelRequestCommand request, CancellationToken cancellationToken)
    {
        var IsThere = await doctorRequestRepository.ExistsByIdAsync(request.RequestId);

        if (!IsThere)
        {
            return Error.NotFound();
        }

        var Doctorrequest = await doctorRequestRepository.GetById(request.RequestId);
        var payment = await paymentRepository.GetpaymentByRequestId(request.RequestId);

        Doctorrequest.Cancel();
        payment.MarkAsCancelled();


        PaymentIntent intent;
        try
        {
            intent= await paymentService.CancelPayment(payment.PaymentProviderId);
        }
        catch (Exception ex)
        {
            return Error.Failure("Payment cancellation failed: " + ex.Message);
        }
        var result = await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            doctorRequestRepository.UpdateDoctorRequest(Doctorrequest);
            paymentRepository.UpdatePayment(payment);

            await Task.CompletedTask;
        });



        return result;
    }
}
