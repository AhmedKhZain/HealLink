using HealLink.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Common.Interfaces.Repositories
{
    public interface IUserTokensRepository
    {

        public  Task AddAsync(UserToken token);

        public Task<UserToken?> GetByTokenAndTypeAsyncAndUserId(string token, TokenTypes type, Guid userId);
        public void Update (UserToken token);


    }
}
