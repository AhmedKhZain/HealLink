using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Infrastructure.Services.Payment;
using MediatR;
using Stripe;

namespace healLink.Application.Requests.Commands.RejectRequest;

public class CancelRequestCommandHandler 
    (IUnitOfWork unitOfWork,
    IDoctorRequestRepository doctorRequestRepository,
    IPaymentRepository paymentRepository,
    IPaymentService paymentService)
    : IRequestHandler<RejectRequestCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(RejectRequestCommand request, CancellationToken cancellationToken)
    {
        var doctorRequest = await doctorRequestRepository.GetById(request.requestId);
        if (doctorRequest == null)
            return Error.NotFound();

        var payment = await paymentRepository.GetpaymentByRequestId(request.requestId);
        if (payment == null)
            return Error.NotFound();




        PaymentIntent intent = null;
        var result = new ErrorOr<Success>();
        try
        {
            intent = await paymentService.CancelPayment(payment.PaymentProviderId);

            payment.MarkAsCancelled();
        }
        catch
        {
            payment.MarkAsFailedToCancel();
        }
        finally
        {
            doctorRequest.Cancel();
            result = await unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                doctorRequestRepository.UpdateDoctorRequest(doctorRequest);
                paymentRepository.UpdatePayment(payment);
                await Task.CompletedTask;
            });

             
        }
        if (result.IsError)
            return result;


        return new Success();
    }
}