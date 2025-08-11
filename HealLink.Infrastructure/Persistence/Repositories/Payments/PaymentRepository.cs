using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Repositories.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HealLinkDbContext _context;
        public PaymentRepository(HealLinkDbContext context)
        {
            _context = context;
        }
        public async Task AddPaymentAsync(Payment paymentToAdd)
        => await _context.Payments.AddAsync(paymentToAdd);

        public async Task<Payment?> GetpaymentById(Guid id)
        => await _context.Payments
            .FirstOrDefaultAsync(p=>p.Id == id);

        public async Task<Payment?> GetpaymentByRequestId(Guid requestId)
        => await _context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.DoctorRequestId == requestId);

        public void UpdatePayment(Payment payment)
        => _context.Payments.Update(payment);
    }
}
