using HealLink.Domain.Doctors;
using HealLink.Domain.PatientDoctorSubscriptions;
using HealLink.Domain.Patients;
using HealLink.Domain.Payments;


namespace HealLink.Domain.Requests
{

    public class DoctorRequest
    {
        public Guid Id { get; private set; }

        public Guid DoctorId { get; private set; }
        public Doctor Doctor { get; private set; } = null!;

        public DoctorSubscriptionPlan Plan { get; private set; }

        public Guid SenderId { get; private set; }
        public Patient Sender { get; private set; } = null!;

        public Payment Payment { get; private set; } = null!;

        public RequestType Type { get; private set; } = RequestType.Initial;
        public string? AttachedFileLink { get; private set; } = null;

        public Guid? SubscriptionId { get; private set; }
        public PatientDoctorSubscription? Subscription { get; private set; }

        public DateTime RequestDate { get; private set; } = DateTime.UtcNow;

        public bool? IsAccepted { get; private set; } = null;
        public bool IsCancelled { get; private set; } = false;
        public DateTime? DecisionDate { get; private set; }

        public bool IsPending => IsAccepted == null;
        public bool IsRejected => IsAccepted == false;
        public bool IsApproved => IsAccepted == true;

        private DoctorRequest() { }



        public DoctorRequest(Guid doctorId, Guid senderId,
            DoctorSubscriptionPlan plan,
            RequestType type,
            Guid? subscriptionId = null,
            string? attachedFilePath=null)
        {
            Id = Guid.NewGuid();
            DoctorId = doctorId;
            SenderId = senderId;
            Type = type;
            SubscriptionId = subscriptionId;
            AttachedFileLink = attachedFilePath;
            Plan = plan;
        }

        public PatientDoctorSubscription Accept()
        {
            if (IsAccepted != null)
                throw new InvalidOperationException();


            if (Type == RequestType.Renewal)
            {
                Subscription.RenewSubscription(TimeSpan.FromDays(Plan.Value));
                IsAccepted = true;
                DecisionDate = DateTime.UtcNow;
            }
            else
            {
                Subscription = new PatientDoctorSubscription(SenderId, DoctorId);
                Subscription.RenewSubscription(TimeSpan.FromDays(Plan.Value));
                SubscriptionId = Subscription.Id;
                IsAccepted = true;
                DecisionDate = DateTime.UtcNow;
            }

            return Subscription!;
        }
        public void SetPayment(Payment payment)
        {
            if (IsAccepted != null)
                throw new InvalidOperationException("This request has already been accepted or rejected.");
            if (IsCancelled)
                throw new InvalidOperationException("This request has already been cancelled.");
            Payment = payment;
        }
        public void SetSubscription(PatientDoctorSubscription subscription)
        {
            SubscriptionId= subscription.Id;
            Subscription = subscription;
        }
        public void Reject()
        {
            if (IsAccepted != null)
                throw new InvalidOperationException();

            IsAccepted = false;
            DecisionDate = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (IsAccepted != null)
                throw new InvalidOperationException();
            if (IsCancelled)
                throw new InvalidOperationException("This request has already been cancelled.");
            IsAccepted = false;
            DecisionDate = DateTime.UtcNow;
        }
    }
    


}
