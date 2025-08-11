using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.MedicalHistories;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Persistence.Repositories.MedicalHistories
{
    public class MedicalHistoryRepository : IMedicalHistoryRepository
    {
        private readonly HealLinkDbContext _context;

        public MedicalHistoryRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task AddMedicalHistoryAsync(MedicalHistory medicalHistory)
            => await _context.MedicalHistories.AddAsync(medicalHistory);

        public void DeleteMedicalHistory(MedicalHistory medicalHistory)
            => _context.MedicalHistories.Remove(medicalHistory);

        public async Task<MedicalHistory> GetMedicalHistoryByIdAsync(Guid id, bool tracking = false)
        {
            var query = _context.MedicalHistories.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByUserIdAsync(Guid userId, int PageSize = 12, int PageNum = 0, bool tracking = false)
        {
            var query = _context.MedicalHistories.Where(m => m.PatientId == userId);
            if (!tracking)
                query = query.AsNoTracking();
            return await query.Skip(PageNum * PageSize).Take(PageSize).ToListAsync();
        }

        public async Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByUserIdAsync(Guid userId, MedicalHistoryType? type=null, int PageSize = 12, int PageNum = 0, bool tracking = false)
        {
            var query = _context.MedicalHistories.Where(m => m.PatientId == userId && (type == null || m.Type == type));
            if (!tracking)
                query = query.AsNoTracking();
            return await query.Skip(PageNum * PageSize).Take(PageSize).ToListAsync();
        }
    }
}