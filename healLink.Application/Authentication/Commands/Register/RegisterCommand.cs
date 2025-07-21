using ErrorOr;

using MediatR;
using HealLink.Application.Authentication.Common;

namespace HealLink.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FullName,
    string ShowName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;