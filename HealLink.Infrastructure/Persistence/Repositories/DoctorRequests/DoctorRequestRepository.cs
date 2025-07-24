using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.Doctors;
using HealLink.Domain.Patients;
using HealLink.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Persistence.Repositories.DoctorRequests
{
    public class DoctorRequestRepository : IDoctorRequestRepository
    {
        private readonly HealLinkDbContext _context;

        public DoctorRequestRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task AddDoctorRequestAsync(DoctorRequest doctorRequest)
        => await _context.DoctorRequests.AddAsync(doctorRequest);

        public void DeleteDoctorRequest(DoctorRequest doctorRequest)
        => _context.DoctorRequests.Remove(doctorRequest);

        public async Task<IEnumerable<DoctorRequest>> GetDoctorRequestsByDoctorIdAsync(Guid doctorId, bool Tracking = false)
        {
            return Tracking
                ? await _context.DoctorRequests
                .Where(d => d.DoctorId == doctorId && d.IsAccepted == null)
                .ToListAsync()
                : await _context.DoctorRequests
                .Where(d => d.DoctorId == doctorId && d.IsAccepted == null)
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<IEnumerable<DoctorRequest>> GetDoctorRequestsByPatientIdAsync(Guid patientId, bool Tracking = false)
        {
            return Tracking
                ? await _context.DoctorRequests
                .Where(d => d.SenderId == patientId && d.IsAccepted == null)
                .ToListAsync()
                : await _context.DoctorRequests
                .Where(d => d.SenderId == patientId && d.IsAccepted == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorRequest?>> GetRequestsHistoryByDoctorIdAsync(Guid doctorId, bool Tracking = false)
        {
            return Tracking
                ? await _context.DoctorRequests
                .Where(d => d.SenderId == doctorId && d.IsAccepted != null)
                .ToListAsync()
                : await _context.DoctorRequests
                .Where(d => d.SenderId == doctorId && d.IsAccepted != null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorRequest?>> GetRequestsHistoryByPatientIdAsync(Guid patientId, bool Tracking = false)
        {
            return Tracking
                ? await _context.DoctorRequests
                .Where(d => d.SenderId == patientId && d.IsAccepted != null)
                .ToListAsync()
                : await _context.DoctorRequests
                .Where(d => d.SenderId == patientId && d.IsAccepted != null)
                .AsNoTracking()
                .ToListAsync();
        }

        public void UpdateDoctorRequest(DoctorRequest doctorRequest)
        => _context.DoctorRequests.Update(doctorRequest);
    }
}
