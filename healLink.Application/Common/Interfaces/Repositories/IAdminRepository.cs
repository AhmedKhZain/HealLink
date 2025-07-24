using HealLink.Domain.Admins;

namespace healLink.Application.Common.Interfaces.Repositories;

public interface IAdminsRepository
{
    Task AddAdminAsync(Admin admin);
    Task<Admin?> GetByIdAsync(Guid adminId, bool Tracking = false);
    Task UpdateAsync(Admin admin);
}