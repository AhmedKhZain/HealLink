using ErrorOr;
using HealLink.Domain.Requests;
using Stripe;

namespace HealLink.Infrastructure.Services.Payment
{
    public interface IPaymentService
    {
        Task<PaymentIntent> AuthorizePayment(string paymentMethodId, DoctorSubscriptionPlan plan);

        Task<PaymentIntent> CapturePayment(string paymentIntentId);

        Task<PaymentIntent> CancelPayment(string paymentIntentId);
        Task<Refund> RefundPayment(string paymentIntentId, long? amountInCents = null);


    }
}

