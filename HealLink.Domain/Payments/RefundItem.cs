using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Payments
{
    public class RefundItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid PaymentId { get; private set; }
        public Payment? payment { get; private set; }
        public string? PaymentProviderId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime RefundDate { get; private set; }=DateTime.UtcNow;
        public string? Reason { get; private set; }


        public RefundItem(Guid paymentId, string paymentProviderId, decimal amount, string? reason = null)
        {
            PaymentId = paymentId;
            PaymentProviderId = paymentProviderId;
            Amount = amount;
            RefundDate = DateTime.UtcNow;
            Reason = reason;
        }
    }

}
