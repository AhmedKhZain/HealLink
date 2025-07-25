using ErrorOr;
using HealLink.Domain.Requests;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Services.Payment
{
	internal class StripePaymentServiceV1_2ErrorOr : IPaymentService
	{
		public async Task<ErrorOr<PaymentIntent>> AuthorizePayment(string currency, string paymentMethodId, DoctorSubscriptionPlan plan)
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
				return Error.Validation(
					code: ex.StripeError?.Code ?? "stripe_error",
					description: ex.StripeError?.Message ?? ex.Message
				);
			}
			return paymentIntent;


		}

		public async Task<ErrorOr<PaymentIntent>> CancelPayment(string paymentIntentId)
		{

			try
			{
				var service = new PaymentIntentService();
				return await service.CancelAsync(paymentIntentId);
			}
			catch (StripeException ex)
			{
				return Error.Validation(
					code: ex.StripeError?.Code ?? "stripe_error",
					description: ex.StripeError?.Message ?? ex.Message
				);
			}


		}

		public async Task<ErrorOr<PaymentIntent>> CapturePayment(string paymentIntentId)
		{
			try
			{

				var service = new PaymentIntentService();
				return await service.CaptureAsync(paymentIntentId);
			}
			catch (StripeException ex)
			{
				return Error.Validation(
					code: ex.StripeError?.Code ?? "stripe_error",
					description: ex.StripeError?.Message ?? ex.Message
				);
			}
		}
	}
}
