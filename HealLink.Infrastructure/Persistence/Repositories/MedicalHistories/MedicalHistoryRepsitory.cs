using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.MedicalHistories;

namespace HealLink.Infrastructure.Persistence.Repositories.MedicalHistories
{
    public class MedicalHistoryRepsitory : IMedicalHistoryRepsitory
    {
        public Task AddMedicalHistoryAsync(MedicalHistory medicalHistory)
        {
            throw new NotImplementedException();
        }

        public void DeleteMedicalHistory(MedicalHistory medicalHistory)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MedicalHistory>?> GetMedicalHistoriesByUserIdAsNoTrackingAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MedicalHistory>?> GetMedicalHistoriesByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<MedicalHistory> GetMedicalHistoryByIdAsNoTrackingAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<MedicalHistory> GetMedicalHistoryByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }





}
