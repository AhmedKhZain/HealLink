
using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HealLink.Infrastructure.Persistence.Repositories.Doctors
{
    internal class DoctorRepository : IDoctorRepository
    {
        private readonly HealLinkDbContext _context;
        public DoctorRepository(HealLinkDbContext context)
        {
            _context = context;
        }
        public async Task DeleteDoctorAsync(Guid doctorId)
        => await _context.Doctors
            .Where(d=>d.Id==doctorId)
            .ExecuteDeleteAsync();

        public async Task<bool> ExistsAsync(Guid doctorId)
        => await _context.Doctors.AnyAsync(d => d.Id == doctorId);

        public async Task<Doctor?> GetDoctorByIdAsync(Guid doctorId, bool tracking = true)
        {
            var query = _context.Doctors;
            if (!tracking)
                query.AsNoTracking();
            return query.FirstOrDefault(d => d.Id == doctorId);
        }

        public void UpdateDoctor(Doctor doctor)
        => _context.Doctors
            .Update(doctor);

        public async Task AddDoctorAsync(Doctor doctor)
        => await _context.Doctors.AddAsync(doctor);


    }
}
