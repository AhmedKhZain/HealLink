using ErrorOr;
using healLink.Application.Common.Interfaces;
using HealLink.Application.Authentication.Common;
using HealLink.Domain.Common;
using HealLink.Domain.Users;
using MediatR;


namespace healLink.Application.Authentication.Commands.PasswordReset
{
    public class ResetPasswordCommandHandler (
        IUsersRepository _usersRepository,
        IUnitOfWork _unitOfWork,
        ITokenGenerator _tokenGenerator,
        IUserTokensRepository _tokenRepsitory,
        IPasswordHasher _pawwwordHasher
        ) : IRequestHandler<ResetPasswordCommand, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _usersRepository.GetByEmailAsync(request.Email);
            if (user is null)
            {
                return Error.Custom(code: "NotFound", description: "No account with this email.", type: 4);
            }

            var token = await _tokenRepsitory.GetByTokenAndTypeAsyncAndUserId(request.Token,TokenTypes.PasswordReset,user.Id);

            if (token is null)
            {
                return Error.Custom(code: "InvalidToken", description: "The provided token is invalid or expired.", type: 4);
            }

            if (token.UserId != user.Id)
            {
                return Error.Custom(code: "TokenMismatch", description: "The token does not match the user.", type: 4);
            }

            var AuthToken = _tokenGenerator.GenerateJwtToken(user);
            user.UpdatePassword(request.NewPassword);
            var passwordHashResult = _pawwwordHasher.HashPassword(request.NewPassword);
            if (passwordHashResult.IsError)
            {
                return passwordHashResult.Errors;
            }
            user.UpdatePassword(
                passwordHashResult.Value
            );
            token.MarkUsed();
           

            _tokenRepsitory.Update(token);
            _usersRepository.UpdateAsync(user);
            await _unitOfWork.CommitChangesAsync();

            var authenticationResult = new AuthenticationResult(
                user,
                AuthToken
            );

            return authenticationResult;

        }
    }
}
