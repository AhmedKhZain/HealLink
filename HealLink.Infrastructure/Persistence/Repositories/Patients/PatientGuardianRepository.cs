using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Patients.Common;
using HealLink.Domain.Patients.HealLink.Domain.Patients;
using HealLink.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Persistence.Repositories.Patients
{
    public class PatientGuardianRepository : IPatientGuardianRepository
    {

        private readonly HealLinkDbContext _context;

        public PatientGuardianRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task AddPatientGuardianAsync(PatientGuardian guardian)
        => await _context.AddAsync(guardian);

        public void AddRelationship(PatientGuardian guardian)
        => _context.Update(guardian);

        public void DeletePatientGuardian(PatientGuardian guardian)
        => _context.Remove(guardian);




        public async Task<IEnumerable<PatientGuardianResponse>?> GetPatientGuardianResponsesByUserIdAsync
            (Guid userId,
            int pageNum,
            int pageSize,
            bool newistFirst = true)
        {
            var query = _context.PatientGuardians
                .AsNoTracking()
                .Where(p => p.PatientId == userId || p.GuardianId == userId)
                .Select(p => new PatientGuardianResponse
                {
                    Id = p.Id,
                    GuardianId = p.GuardianId,
                    GuardianName = p.Guardian.User.NameToShow,
                    GuardianPhotoLink = p.Guardian.User.ProfilePhotoLink,
                    PatientId = p.PatientId,
                    PatientName = p.Patient.User.NameToShow,
                    PatientPhotoLink = p.Patient.User.ProfilePhotoLink,
                    Relationship = p.RelationshipType ?? "",
                    StartedAt = p.CreatedAt
                });

        query = newistFirst
            ? query.OrderByDescending(p => p.StartedAt)
            : query.OrderBy(p => p.StartedAt);

        return await query
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }




        public async Task<PatientGuardian?> GetPatientGuardianByIdAsync(Guid patientguardianId, bool Traking=false)
        {
            IQueryable<PatientGuardian> query = _context.PatientGuardians!;
            if (!Traking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(pg => pg.Id == patientguardianId);
        }
    }
}
