using ErrorOr;
using HealLink.Domain.Requests;
using Stripe;

namespace HealLink.Infrastructure.Services.Payment
{
    public interface IPaymentService
    {
        public Task<PaymentIntent> AuthorizePayment(string currency, string paymentMethodId, DoctorSubscriptionPlan plan);

        public Task<PaymentIntent> CapturePayment(string paymentIntentId);

        public Task<PaymentIntent> CancelPayment(string paymentIntentId);





    }
}