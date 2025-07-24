using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.Patients.HealLink.Domain.Patients;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Persistence.Repositories.Patients
{
    public class PatientGuardianRepository : IPatientGuardianRepository
    {

        private readonly HealLinkDbContext _Context;
        public PatientGuardianRepository(HealLinkDbContext context)
        {
            _Context = context;
        }

        public async Task AddPatientGuardianAsync(PatientGuardian guardian)
        => await _Context.AddAsync(guardian);

        public void AddRelationship(PatientGuardian guardian)
        => _Context.Update(guardian);

        public void DeletePatientGuardian(PatientGuardian guardian)
        =>_Context.Remove(guardian);

        public async Task<IEnumerable<PatientGuardian?>> GetAllPatientGuardianByPatientIdAsync(Guid Patientid, bool Tracking = false)
        {
            return Tracking 
                ? await _Context.PatientGuardians
                    .Where(p => p.PatientId == Patientid)
                    .ToListAsync()
                : await _Context.PatientGuardians
                    .Where(p => p.PatientId == Patientid)
                    .AsNoTracking()
                    .ToListAsync();

        }

        public async Task<PatientGuardian?> GetPatientGuardianAsync(Guid id, bool Tracking = false)
        {
            return Tracking
                ? await _Context.PatientGuardians
                    .FirstOrDefaultAsync(p => p.Id == id)
                : await _Context.PatientGuardians
                    .AsNoTracking()
                    .FirstOrDefaultAsync((p => p.Id == id));


        }

        public async Task<IEnumerable<PatientGuardian?>> GetPatientGuardiansByPatientIdAsync(Guid Patientid, int PageNum, int PageSize, bool NewistFirst = true, bool Tracking = false)
        {
            return Tracking
                ? await _Context.PatientGuardians
                    .Where(P => P.PatientId == Patientid)
                    .Skip((PageNum - 1) * PageSize).Take(PageSize)
                    .ToListAsync()
                : await _Context.PatientGuardians
                    .Where(P => P.PatientId == Patientid)
                    .Skip((PageNum - 1) * PageSize).Take(PageSize)
                    .AsNoTracking()
                    .ToListAsync();


        }
    }
}
