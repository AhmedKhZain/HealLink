using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.PatientDoctorSubscriptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Repositories.PatientDoctorSubscriptions
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly HealLinkDbContext _context;
        public SubscriptionRepository(HealLinkDbContext context)
        {
            _context = context;
        }
        public async Task AddSubscriptionAsync(PatientDoctorSubscription subscription)
        => await _context.PatientDoctorSubscriptions
            .AddAsync(subscription);

        public  void DeleteSubscription(PatientDoctorSubscription subscription)
        => _context.PatientDoctorSubscriptions
            .Remove(subscription);

        public async Task<bool> ExistsActiveByPatientIdAndDoctorIdAsync(Guid patientId, Guid doctorId)
        => await _context.PatientDoctorSubscriptions
            .AnyAsync(s => s.PatientId == patientId && s.DoctorId == doctorId&& s.ExpiryDate>DateTime.UtcNow );

        public async Task<PatientDoctorSubscription?> GetById(Guid subscriptionId, bool Tracking = false)
        {
            var query = _context.PatientDoctorSubscriptions;

            if (!Tracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(s => s.Id == subscriptionId);

        }

        public async Task<IEnumerable<PatientDoctorSubscription>?> GetDoctorHistoryByIdAsync(Guid doctorId, bool Tracking = false)
        {
            var query= _context.PatientDoctorSubscriptions.Where(s=>s.DoctorId == doctorId && s.ExpiryDate.Value < DateTime.Now);
            if (!Tracking)
                query= query.AsNoTracking();
            return await query.ToListAsync();

        }

        public async Task<IEnumerable<PatientDoctorSubscription>?> GetSubscriptionsByDoctorIdAsync(Guid doctorId, bool Tracking = false)
        {
            return Tracking
                ? await _context.PatientDoctorSubscriptions
                    .Where(s => s.DoctorId == doctorId && s.ExpiryDate.Value > DateTime.Now)
                    .ToListAsync()
                : await _context.PatientDoctorSubscriptions
                    .AsNoTracking()
                    .Where(s => s.DoctorId == doctorId && s.ExpiryDate.Value > DateTime.Now)
                    .ToListAsync();

        }

        public async Task<PatientDoctorSubscription?> GetSubscriptionsByPatientIdAndDoctorIdAsync(Guid patientId, Guid doctorId, bool Tracking = false)
        {
            return Tracking
                ? await _context.PatientDoctorSubscriptions
                    .FirstOrDefaultAsync(s => s.PatientId == patientId && s.DoctorId == doctorId)
                : await _context.PatientDoctorSubscriptions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.PatientId == patientId && s.DoctorId == doctorId);
        }

        public async Task<IEnumerable<PatientDoctorSubscription>?> GetSubscriptionsByPatientIdAsync(Guid patientId, bool Tracking = false)
        {
            return Tracking
                ? await _context.PatientDoctorSubscriptions
                    .Where(s => s.PatientId == patientId && s.ExpiryDate.Value > DateTime.Now)
                    .ToListAsync()
                : await _context.PatientDoctorSubscriptions
                    .AsNoTracking()
                    .Where(s => s.PatientId == patientId && s.ExpiryDate.Value > DateTime.Now)
                    .ToListAsync();
        }

        public void UpdateSubscription(PatientDoctorSubscription subscription)
        {
            _context.PatientDoctorSubscriptions
                .Update(subscription);
        }
    }
}
