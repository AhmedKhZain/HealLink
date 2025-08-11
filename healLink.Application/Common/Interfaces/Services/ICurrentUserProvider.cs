using ErrorOr;
using HealLink.Application.Common.Models;

namespace healLink.Application.Common.Interfaces.Service;

public interface ICurrentUserProvider
{
    ErrorOr<CurrentUser> GetCurrentUser();
}