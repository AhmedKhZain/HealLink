using healLink.Application.Patients.Common;
using HealLink.Domain.Patients;
using HealLink.Domain.Patients.HealLink.Domain.Patients;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IPatientGuardianRepository
    {
        Task AddPatientGuardianAsync(PatientGuardian guardian);


        void AddRelationship(PatientGuardian guardian);

        void DeletePatientGuardian(PatientGuardian guardian);


        Task<IEnumerable<PatientGuardianResponse>?> GetPatientGuardianResponsesByUserIdAsync
            (Guid userId,
            int pageNum,
            int pageSize,
            bool newistFirst = true);


        Task<PatientGuardian?> GetPatientGuardianByIdAsync(Guid patientguardianId, bool Tracking=false);
    }



}