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
    public class RefundRepository : IRefundRepository
    {
        private readonly HealLinkDbContext _context;

        public RefundRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task AddRefundAsync(RefundItem refund)
        => await _context.RefundItems.AddAsync(refund);

        public void DeleteRefundAsync(RefundItem refund)
        => _context.RefundItems.Remove(refund);
    }
}
