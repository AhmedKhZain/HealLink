//using healLink.Application.Common.Interfaces.Repositories;
//using HealLink.Domain.MedicalHistories;
//using Microsoft.EntityFrameworkCore;

//namespace HealLink.Infrastructure.Persistence.Repositories.MedicalHistories
//{
//    public class MedicalHistoryRepository : IMedicalHistoryRepository
//    {
//        private readonly HealLinkDbContext _context;

//        public MedicalHistoryRepository(HealLinkDbContext context)
//        {
//            _context = context;
//        }

//        public async Task AddMedicalHistoryAsync(MedicalHistory medicalHistory)
//            => await _context.MedicalHistories.AddAsync(medicalHistory);

//        public void DeleteMedicalHistory(MedicalHistory medicalHistory)
//            => _context.MedicalHistories.Remove(medicalHistory);

//        public async Task<MedicalHistory> GetMedicalHistoryByIdAsync(Guid id, bool tracking = false)
//            => tracking
//                ? await _context.MedicalHistories.FirstOrDefaultAsync(m => m.Id == id)
//                : await _context.MedicalHistories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

//        public async Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByUserIdAsync(Guid userId, int PageSize = 12, int PageNum = 0, bool tracking = false)
//            => tracking
//                ? await _context.MedicalHistories
//                .Where(m => m.PatientId == userId)
//                .Skip((PageSize - 1) * PageNum).Take(PageSize)
//                .ToListAsync()
//                : await _context.MedicalHistories.AsNoTracking()
//                .Where(m => m.PatientId == userId)
//                .Skip((PageSize - 1) * PageNum).Take(PageSize)
//                .ToListAsync();
//        public async Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByUserIdAsync(Guid userId, MedicalHistoryType Type, int PageSize = 12, int PageNum = 0, bool tracking = false)
//            => tracking
//                ? await _context.MedicalHistories
//                .Where(m => m.PatientId == userId && Type == m.Type)
//                .Skip((PageSize - 1) * PageNum).Take(PageSize)
//                .ToListAsync()
//                : await _context.MedicalHistories.AsNoTracking()
//                .Where(m => m.PatientId == userId && Type == m.Type)
//                .Skip((PageSize - 1) * PageNum).Take(PageSize)
//                .ToListAsync();
//    }
//}
