using healLink.Application.Authentication.Commands.UpdateUserData;
using HealLink.Application.Authentication.Commands.Register;
using HealLink.Application.Authentication.Common;

namespace HealLink.Api.Authentication
{
    // RegisterMappingExtensions.cs
    public static class AuthenticationMappingExtensions
    {
        public static RegisterCommand ToCommand(this RegisterRequest request)
            => new(request.FullName, request.NameToShow, request.Email, request.Password);



        public static AuthResponse FromAuthenticationResult(this AuthenticationResult authResult)
            => new(
                authResult.User.FullName,
                authResult.User.NameToShow,
                authResult.User.Email,
                authResult.Token);


        public static UpdateUserDataCommand ToUpdateUserDataCommand(this UpdateUserDataRequest request)
            => new(request.Id, request.ShowName, request.FullName, request.PhotoPath, request.Email);
    }

}
