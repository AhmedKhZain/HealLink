namespace HealLink.Infrastructure.Services.Payment
{
    public class StripeMethodSettings
    {
        public string PaymentMethodId { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string PaymentIntentId { get; set; } = string.Empty;
    }


}
