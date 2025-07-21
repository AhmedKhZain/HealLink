using ErrorOr;
using HealLink.Domain.Doctors;
using HealLink.Domain.PatientDoctorSubscriptions;
using HealLink.Domain.Patients;
using HealLink.Domain.Payments;
using Microsoft.VisualBasic;
using System;
using System.Reflection.Metadata;

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
            RequestType type, Guid? subscriptionId = null,
            string? attachedFilePath=null)
        {
            DoctorId = doctorId;
            SenderId = senderId;
            Type = type;
            SubscriptionId = subscriptionId;
            AttachedFileLink = attachedFilePath;
            Plan = plan;
        }

        public ErrorOr<PatientDoctorSubscription> Accept()
        {
            if (IsAccepted != null)
                return Error.Custom(code: "processed",
                    description: "This request has already been processed.",
                    type:1);




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

        public ErrorOr<Success> Reject()
        {
            if (IsAccepted != null)
                return Error.Custom(code: "processed",
                    description: "This request has already been processed.",
                    type: 1);

            IsAccepted = false;
            DecisionDate = DateTime.UtcNow;
            return new Success();
        }
        public ErrorOr<Success> Cancel()
        {
            if (IsAccepted != null)
                return Error.Custom(code: "processed",
                    description: "This request has already been processed.",
                    type: 1);
            if (IsCancelled)
                return Error.Custom(code: "already_cancelled",
                    description: "This request has already been cancelled.",
                    type: 1);
            IsCancelled = true;
            DecisionDate = DateTime.UtcNow;
            return new Success();
        }
    }
    


}
