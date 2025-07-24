using ErrorOr;
using HealLink.Domain.Doctors;
using HealLink.Domain.Patients;
using HealLink.Domain.Payments;
using HealLink.Domain.Prescriptions;
using HealLink.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.PatientDoctorSubscriptions
{
    public class PatientDoctorSubscription
    {
        public Guid Id { get; private set; }

        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; } = null!;

        public Guid DoctorId { get; private set; }
        public Doctor Doctor { get; private set; } = null!;


        public DateTime SubscribedAt { get; private set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; private set; }



        public ICollection<PatientDoctorSubscriptionChatMessage> ChatMassages 
            { get; private set; } = new List<PatientDoctorSubscriptionChatMessage>();

        public ICollection<DoctorRequest> DoctorRequests
            { get; private set; } = new List<DoctorRequest>();
        public ICollection<Prescription> Prescriptions
            { get; private set; } = new List<Prescription>();




        private PatientDoctorSubscription() { }

        public PatientDoctorSubscription(Guid patientId, Guid doctorId)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            DoctorId = doctorId;


        }


        public void RenewSubscription(TimeSpan duration)
        {

            if (ExpiryDate.HasValue)
            {
                ExpiryDate = ExpiryDate.Value.Add(duration);
            }
            else
            {
                ExpiryDate = DateTime.UtcNow.Add(duration);
            }
        }
    }

}
