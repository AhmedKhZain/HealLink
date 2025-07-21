using HealLink.Domain.PatientDoctorSubscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Common
{
    public class SubscriptionRenewal
    {
        public Guid Id { get; private set; }
        public Guid SubscriptionId { get; private set; }
        public PatientDoctorSubscription Subscription { get; private set; } = null!;
        public DateTime RenewalDate { get; private set; } = DateTime.UtcNow;
        public TimeSpan Duration { get; private set; }

        private SubscriptionRenewal() { }

        public SubscriptionRenewal(Guid subscriptionId, TimeSpan duration)
        {
            Id = Guid.NewGuid();
            SubscriptionId = subscriptionId;
            Duration = duration;
        }
    }

}
