using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Repositories.Patients
{
    public class PatientRepository : IPatientRepository
    {

        private readonly HealLinkDbContext _context;
        public PatientRepository(HealLinkDbContext context)
        {
            _context = context;
        }


        public async Task AddPatientAsync(Patient patient)
        => await _context.Patients.AddAsync(patient);

        

        public void DeletePatient(Patient Patient)
        => _context.Patients.Remove(Patient);
        

        public async Task<bool> ExistsByUserIdAsync(Guid userId)
            =>await _context.Patients
                .AnyAsync(p => p.Id == userId);




        public async Task<Patient?> GetPatientByUserIdAsync(Guid userId, bool Tracking = false)
        {
            return Tracking ? await _context.Patients
                    .FirstOrDefaultAsync(p => p.Id == userId)
                    : await _context.Patients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == userId);
        }

    }
}
