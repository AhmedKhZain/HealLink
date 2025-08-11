using HealLink.Domain.Patients;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        Task AddPatientAsync(Patient patient);

        Task<Patient?> GetPatientByUserIdAsync(Guid userId, bool Tracking = false);
        Task<bool> ExistsByUserIdAsync(Guid userId);
        void DeletePatient(Patient Patient);

    }



}