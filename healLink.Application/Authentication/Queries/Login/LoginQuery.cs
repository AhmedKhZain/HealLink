using ErrorOr;

using MediatR;
using HealLink.Application.Authentication.Common;

namespace HealLink.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;