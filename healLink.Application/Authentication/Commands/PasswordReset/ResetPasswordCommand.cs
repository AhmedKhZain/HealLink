using ErrorOr;
using HealLink.Application.Authentication.Common;
using MediatR;


namespace healLink.Application.Authentication.Commands.PasswordReset;

public partial record ResetPasswordCommand(
    string Email,
    string Token,
    string NewPassword
) : IRequest<ErrorOr<AuthenticationResult>>;


