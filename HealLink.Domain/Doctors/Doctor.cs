using HealLink.Domain.Admins;
using HealLink.Domain.PatientDoctorSubscriptions;
using HealLink.Domain.Prescriptions;
using HealLink.Domain.Requests;
using HealLink.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Domain.Doctors
{
    public class Doctor
    {
        public Guid Id { get; private set; }
        public string SyndicateIdImageLink { get; private set; } = null!;
        public string NationalID { get; private set; } = null!;
        public string PracticeLicenseNumber { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public bool IsApproved { get; private set; } = false;
        public DateTime? ApprovedAt { get; private set; } = null!;
        public Guid? ApprovedByAdminId { get; private set; } = null!;
        public Admin Admin { get; private set; } = null!;
        public ICollection<PatientDoctorSubscription> Subscriptions { get; private set; } = new List<PatientDoctorSubscription>();
        public ICollection<DoctorRequest> DoctorRequests { get; private set; } = new List<DoctorRequest>();
        public User User { get; private set; } = null!;
        

        public Doctor(Guid userId, string syndicateIdImageLink, string nationalId, string licenseNumber)
        {
            Id = userId;
            SyndicateIdImageLink = syndicateIdImageLink;
            NationalID = nationalId;
            PracticeLicenseNumber = licenseNumber;
        }
        private Doctor() { }

    }
}
