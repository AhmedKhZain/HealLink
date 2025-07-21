using healLink.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Persistence.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly HealLinkDbContext _context;

        public UsersRepository(HealLinkDbContext context)
        {
            _context = context;
        }


        public async Task AddUserAsync(User user)
            => await _context.Users.AddAsync(user);




        public async Task<bool> ExistsByEmailAsync(string email)
        => await _context.Users.AnyAsync(u => u.Email == email);




        public Task<User?> GetByEmailAsync(string email)
        => _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByEmailAsNoTrckingAsync(string email)
        => await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);




        public async Task<User?> GetByIdAsync(Guid userId)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        public void UpdateAsync(User user)
        => _context.Users
            .Update(user);

 
    }
}
