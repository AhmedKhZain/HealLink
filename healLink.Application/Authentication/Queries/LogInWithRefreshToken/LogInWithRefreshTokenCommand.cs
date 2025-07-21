using ErrorOr;
using HealLink.Application.Authentication.Common;
using MediatR;
namespace healLink.Application.Authentication.Queries.LogInWithRefreshToken;
public record LogInWithRefreshTokenQuery(
    string RefreshToken,Guid UserId) : IRequest<ErrorOr<AuthenticationResult>>;

