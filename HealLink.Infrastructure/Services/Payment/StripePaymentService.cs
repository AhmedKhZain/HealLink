using ErrorOr;
using HealLink.Domain.Requests;
using Microsoft.Extensions.Options;
using Stripe;

namespace HealLink.Infrastructure.Services.Payment
{
    internal class StripePaymentService : IPaymentService
    {
        private readonly StripeSettings _settings;

        public StripePaymentService(IOptions<StripeSettings> stripeOptions)
        {
            _settings = stripeOptions.Value;

            StripeConfiguration.ApiKey = _settings.SecretKey;
        }

        public async Task<PaymentIntent> AuthorizePayment(string paymentMethodId, DoctorSubscriptionPlan plan)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(plan.Price * 100),
                Currency = _settings.Currency,
                PaymentMethod = paymentMethodId,
                ConfirmationMethod = "manual",
                CaptureMethod = "manual",
                Confirm = true,
                PaymentMethodTypes = new List<string>
                {
                    "card"
                }
            };

            var service = new PaymentIntentService();
            try
            {

                return await service.CreateAsync(options);
            }
            catch (StripeException ex)
            {
                Console.WriteLine("Stripe error:");
                Console.WriteLine($"Type: {ex.StripeError?.Type}");
                Console.WriteLine($"Code: {ex.StripeError?.Code}");
                Console.WriteLine($"Message: {ex.StripeError?.Message}");
                Console.WriteLine($"PaymentIntentId: {ex.StripeError?.PaymentIntent?.Id}");

                throw;
            }
        }



        public async Task<PaymentIntent> CancelPayment(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            try
            {
                return await service.CancelAsync(paymentIntentId);
            }
            catch (StripeException ex)
            {
                throw;
            }
        }

        public async Task<PaymentIntent> CapturePayment(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            try
            {
                return await service.CaptureAsync(paymentIntentId);
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"{ex.StripeResponse.ToString()}");
                Console.WriteLine($"{ex.StripeError.ToString()}");
                Console.WriteLine($"{ex.ToString()}");
                throw;
            }
        }
        public async Task<Refund> RefundPayment(string paymentIntentId, long? amountInCents = null)
        {
            var service = new RefundService();
            var options = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId,
                Amount = amountInCents,
            };
            Refund refund;
            try
            {
                refund= await service.CreateAsync(options);

            }
            catch(StripeException ex)
            {
                Console.WriteLine("\n\n\n\n\n\n\nStripe Refund Error:");
                Console.WriteLine($"Type: {ex.StripeError?.Type}");
                Console.WriteLine($"Code: {ex.StripeError?.Code}");
                Console.WriteLine($"Message: {ex.StripeError?.Message}\n\n\n\n\n\n\n");
                throw;
            }
            return refund;
        }


    }
}
