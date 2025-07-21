using healLink.Application.Common.Interfaces;
using HealLink.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Persistence.Repositories.Users
{
    public class UserTokensRepository : IUserTokensRepository
    {
        private readonly HealLinkDbContext _context;

        public UserTokensRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserToken token)
            => await _context.UserTokens.AddAsync(token);



        //public async Task<UserToken?> GetByTokenAndTypeAsync(string token, TokenTypes type)
        //    => await _context.UserTokens
        //        .FirstOrDefaultAsync(t => t.Token == token && t.Type == type);

        public Task<UserToken?> GetByTokenAndTypeAsyncAndUserId(string token, TokenTypes type, Guid userId)
            => _context.UserTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Token == token && t.Type == type );

        //public async Task<UserToken?> GetByTokenAsync(string token)
        //    => await _context.UserTokens
        //        .FirstOrDefaultAsync(t => t.Token == token);


        public void Update(UserToken token)
            => _context.UserTokens
                .Update(token);
    }

}
