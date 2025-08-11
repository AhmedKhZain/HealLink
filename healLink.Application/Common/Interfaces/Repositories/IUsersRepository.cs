using HealLink.Domain.Users;

namespace healLink.Application.Common.Interfaces.Repositories;

public interface IUsersRepository
{
    Task AddUserAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByEmailAsync(string email, bool tracking = false);
    Task<User?> GetByIdAsync(Guid userId, bool tracking = false);
    void Update(User user);
}