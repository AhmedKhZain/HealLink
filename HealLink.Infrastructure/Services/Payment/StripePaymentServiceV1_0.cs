using ErrorOr;
using HealLink.Domain.Requests;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Services.Payment
{
    internal class StripePaymentServiceV1_0 : IPaymentService
    {

        private readonly StripeSettings settings;
        public StripePaymentServiceV1_0(IOptions<StripeSettings> stripeOptions)
        {
            settings = stripeOptions.Value;
        }

        public async Task<PaymentIntent> AuthorizePayment(string currency, string paymentMethodId, DoctorSubscriptionPlan plan)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(plan.Price * 100), // Convert to cents
                Currency = currency,
                PaymentMethod = paymentMethodId,
                ConfirmationMethod = "manual",
                Confirm = true,
            };


            var service = new PaymentIntentService();
            var paymentIntent = new PaymentIntent();
            try
            {
                paymentIntent = await service.CreateAsync(options);
            }
            catch (StripeException ex)
            {
                throw new StripeException();

            }
            return paymentIntent;


        }

        public async Task<PaymentIntent> CancelPayment(string paymentIntentId)
        {

            try
            {
                var service = new PaymentIntentService();
                return await service.CancelAsync(paymentIntentId);
            }
            catch (StripeException ex)
            {
                throw new StripeException();

            }


        }

        public async Task<PaymentIntent> CapturePayment(string paymentIntentId)
        {
            try
            {

                var service = new PaymentIntentService();
                return await service.CaptureAsync(paymentIntentId);
            }
            catch (StripeException ex)
            {
                throw new StripeException();
            }
        }
    }
}
