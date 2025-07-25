using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Users;
using MediatR;


namespace healLink.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler(
        IUsersRepository _usersRepository,
        IUnitOfWork _unitOfWork,
        IUserTokensRepository _tokenRepsitory)
        : IRequestHandler<ConfirmEmailCommand, ErrorOr<Success>>


    {
        public async Task<ErrorOr<Success>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var Token = await _tokenRepsitory.GetByTokenAndTypeAsyncAndUserId(request.Token,TokenTypes.EmailConfirmation,request.UserId);

            
            var user = await _usersRepository.GetByEmailAsync(request.Email);

            if (Token is null
                || user is null 
                || !(user.Id==Token.UserId))
            {
                return Error.Custom(code:"NotFound",description:"No, Some Thing Went Worng" ,type:3);
            }

            if (Token.IsExpired())
                return Error.Custom(code: "Expired", description: "The token has expired.", type: 3);

            user.ConfirmEmail();
            Token.MarkUsed();
            var result = await _unitOfWork.ExecuteInTransactionAsync(() =>
            {
                _tokenRepsitory.Update(Token);
                return Task.CompletedTask;
            });

            if (result.IsError)
                return result.Errors;

            return new Success();

        }
    }
}
