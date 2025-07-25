using ErrorOr;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Application.Common.Models;

namespace HealLink.Api.Services
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ErrorOr<CurrentUser> GetCurrentUser()
        {
            var id = Guid.Parse(_httpContextAccessor.HttpContext!.User.Claims
                .SingleOrDefault(claim => claim.Type == "id")
                .Value);

            var role = _httpContextAccessor.HttpContext.User.Claims
                .SingleOrDefault(claim => claim.Type == "role")
                .Value;

            if (id == Guid.Empty || string.IsNullOrEmpty(role))
            {
                return Error.Unauthorized(description: "User is not authenticated");
            }

            return new CurrentUser(id, role);


        }
    }
}
