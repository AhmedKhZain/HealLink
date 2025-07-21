//using healLink.Application.Common.Interfaces;
//using HealLink.Application.Common.Models;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace HealLink.Infrastructure.Services
//{
//    public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
//    {
//        public CurrentUser GetCurrentUser()
//        {
//            _httpContextAccessor.HttpContext.ThrowIfNull();

//            var id = GetClaimValues("id")
//                .Select(Guid.Parse)
//                .First();

//            var permissions = GetClaimValues("permissions");
//            var roles = GetClaimValues(ClaimTypes.Role);

//            return new CurrentUser(Id: id, Permissions: permissions, Roles: roles);
//        }

//        private IReadOnlyList<string> GetClaimValues(string claimType)
//        {
//            return _httpContextAccessor.HttpContext!.User.Claims
//                .Where(claim => claim.Type == claimType)
//                .Select(claim => claim.Value)
//                .ToList();
//        }
//    }
//}
