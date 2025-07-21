using HealLink.Domain.PatientDoctorSubscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.PatientDoctorSubscriptionChatMessages
{
    public class PatientDoctorSubscriptionChatMessage
    {
   

        public Guid Id { get; private set; }
        public Guid PatientDoctorSubscriptionId { get; private set; }
        public PatientDoctorSubscription PatientDoctorSubscription { get; private set; } = null!;

        public DateTime DateTime { get; private set; } = DateTime.UtcNow;
        public string Message { get; private set; } = null!;

        public bool IsFromPatient { get; private set; } = true;



        private PatientDoctorSubscriptionChatMessage() { }
        public PatientDoctorSubscriptionChatMessage(Guid id, Guid SubscriptionId,string message)
        {
            Id = id;
            PatientDoctorSubscriptionId = SubscriptionId;
            Message = message;
        }


    }
}
