using ErrorOr;
using HealLink.Domain.MedicalHistories;
using HealLink.Domain.PatientDoctorSubscriptions;
using HealLink.Domain.Patients.HealLink.Domain.Patients;
using HealLink.Domain.Requests;
using HealLink.Domain.Users;
using Microsoft.VisualBasic;


namespace HealLink.Domain.Patients
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedAt { get; private set; } = null!;

        public ICollection<PatientDoctorSubscription> Subscriptions { get; private set; } = new List<PatientDoctorSubscription>();
        public ICollection<MedicalHistory> MedicalHistories { get; private set; } = new List<MedicalHistory>();
        public ICollection<DoctorRequest> DoctorRequests { get; private set; } = new List<DoctorRequest>();
        public ICollection<PatientGuardian> Guardians { get; private set; } = new List<PatientGuardian>();

        public User User { get; private set; } = null!;

        private Patient() { }

        public Patient(Guid userId)
        {
            Id = userId;
        }

        public void Delete()
        {
            if (IsDeleted)
            {
                throw new InvalidOperationException("already deleted") ;
            }
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
