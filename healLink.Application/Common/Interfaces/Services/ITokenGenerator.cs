using HealLink.Domain.Users;

namespace healLink.Application.Common.Interfaces.Service;

public interface ITokenGenerator
{
    UserToken GenerateUserTokens(User user, TokenTypes type);
    string GenerateJwtToken(User user);

}