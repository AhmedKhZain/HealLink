using ErrorOr;

using MediatR;
using HealLink.Application.Authentication.Common;
using healLink.Application.Common.Interfaces;
using HealLink.Domain.Common;


namespace HealLink.Application.Authentication.Queries.Login;

public class LoginQueryHandler(
    ITokenGenerator _tokenGenerator,
    IPasswordHasher _passwordHasher,
    IUsersRepository _usersRepository)
        : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByEmailAsync(query.Email);

        return user is null || !user.IsCorrectPasswordHash(query.Password, _passwordHasher)
            ? AuthenticationErrors.InvalidCredentials
            : new AuthenticationResult(user, _tokenGenerator.GenerateJwtToken(user));
    }
}