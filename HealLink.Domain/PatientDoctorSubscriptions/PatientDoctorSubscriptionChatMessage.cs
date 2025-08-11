using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.PatientDoctorSubscriptions
{
    public class PatientDoctorSubscriptionChatMessage
    {
   

        public Guid Id { get; private set; }
        public Guid PatientDoctorSubscriptionId { get; private set; }
        public PatientDoctorSubscription PatientDoctorSubscription { get; private set; } = null!;

        public DateOnly MassageDate { get;private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public TimeOnly MassageTime { get; private set; } = TimeOnly.FromDateTime(DateTime.UtcNow);
        public DateTime? MassageDateTime { get; private set; } =null;
        public string Message { get; private set; } = null!;
        public string? AttachmentLink { get; private set; } = null;

        public bool IsFromPatient { get; private set; } = true;



        private PatientDoctorSubscriptionChatMessage() { }
        public PatientDoctorSubscriptionChatMessage(Guid id, Guid SubscriptionId,string message,bool FromPatient=true,string attachmentLink=null)
        {
            Id = id;
            PatientDoctorSubscriptionId = SubscriptionId;
            Message = message;
            IsFromPatient = FromPatient;
            AttachmentLink = attachmentLink;
        }


    }
}
