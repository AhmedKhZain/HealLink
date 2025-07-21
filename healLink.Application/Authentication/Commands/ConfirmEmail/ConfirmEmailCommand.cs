using ErrorOr;
using HealLink.Application.Authentication.Common;
using MediatR;

namespace healLink.Application.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand (string Email,string Token,Guid UserId) : IRequest<ErrorOr<Success>>;

