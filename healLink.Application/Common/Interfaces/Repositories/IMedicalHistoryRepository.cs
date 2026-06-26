using HealLink.Domain.MedicalHistories;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IMedicalHistoryRepository
    {
        Task AddMedicalHistoryAsync(MedicalHistory medicalHistory);
        Task<MedicalHistory> GetMedicalHistoryByIdAsync(Guid Id, bool tracking = false);
        Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByUserIdAsync(Guid userId, int PageSize = 12, int PageNum = 0, bool tracking = false);
        Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByUserIdAsync(Guid userId, MedicalHistoryType? Type=null, int PageSize = 12, int PageNum = 0, bool tracking = false);
        void DeleteMedicalHistory(MedicalHistory medicalHistory);
    }

}