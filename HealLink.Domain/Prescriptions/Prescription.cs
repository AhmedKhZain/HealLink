using HealLink.Domain.Doctors;
using HealLink.Domain.PatientDoctorSubscriptions;

namespace HealLink.Domain.Prescriptions
{
    public class Prescription
    {
        public Guid Id { get; private set; }

        public Guid SubsciptionId { get; private set; }
        public PatientDoctorSubscription Subscription { get; private set; } = null!;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string PrescriptionText { get; private set; } = null!;

        public ICollection<Medication> Medications { get; private set; } = new List<Medication>();
    }
}