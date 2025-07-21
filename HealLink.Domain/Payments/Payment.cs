using HealLink.Domain.Requests;

namespace HealLink.Domain.Payments
{
    public partial class Payment
    {
        public Guid Id { get; private set; }

        public Guid DoctorRequestId { get; private set; }
        public DoctorRequest DoctorRequest { get; private set; } = null!;

        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; } = PaymentStatus.Authorized;
        public DateTime? DoneAt { get; private set; }
        public string? PaymentProviderId { get; private set; }
        public DateTime PaidAt { get; private set; }

        private Payment() { }

        public Payment(Guid doctorRequestId, decimal amount, PaymentStatus status, string? paymentProviderId)
        {
            Id = Guid.NewGuid();
            DoctorRequestId = doctorRequestId;
            Amount = amount;
            Status = status;
            PaymentProviderId = paymentProviderId;
            PaidAt = DateTime.UtcNow;
        }
        public void MarkAsCaptured()
        {
            Status = PaymentStatus.Captured;
            DoneAt = DateTime.UtcNow;
        }
        public void MarkAsFailed()
        {
            Status = PaymentStatus.Failed;
            DoneAt = DateTime.UtcNow;
        }


    }
}