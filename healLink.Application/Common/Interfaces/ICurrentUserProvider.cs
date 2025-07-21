using HealLink.Application.Common.Models;

namespace healLink.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}