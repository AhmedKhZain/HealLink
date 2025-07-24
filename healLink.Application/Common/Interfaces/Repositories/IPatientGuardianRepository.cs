using HealLink.Domain.Patients;
using HealLink.Domain.Patients.HealLink.Domain.Patients;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IPatientGuardianRepository
    {
        Task AddPatientGuardianAsync(PatientGuardian guardian);


        Task<PatientGuardian?> GetPatientGuardianAsync(Guid id,bool Tracking=false);
        Task<IEnumerable<PatientGuardian?>> GetAllPatientGuardianByPatientIdAsync(Guid Patientid, bool Tracking=false);
        Task<IEnumerable<PatientGuardian?>> GetPatientGuardiansByPatientIdAsync(Guid Patientid, int PageNum, int PageSize, bool NewistFirst = true, bool Tracking=false);

        
        void AddRelationship(PatientGuardian guardian);

        void DeletePatientGuardian(PatientGuardian guardian);


    }



}