using HealLink.Domain.MedicalHistories;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IMedicalHistoryRepsitory
    {
        Task AddMedicalHistoryAsync(MedicalHistory medicalHistory);
        Task<MedicalHistory> GetMedicalHistoryByIdAsNoTrackingAsync(Guid Id);
        Task<MedicalHistory> GetMedicalHistoryByIdAsync(Guid Id);
        Task<IEnumerable<MedicalHistory>?> GetMedicalHistoriesByUserIdAsNoTrackingAsync(Guid userId);
        Task<IEnumerable<MedicalHistory>?> GetMedicalHistoriesByUserIdAsync(Guid userId);
        void DeleteMedicalHistory(MedicalHistory medicalHistory);

    }



}