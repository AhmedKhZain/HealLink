using ErrorOr;
using healLink.Application.Common.Interfaces;
using HealLink.Application.Common.Helpers;
using HealLink.Domain.Users;
using MediatR;


namespace healLink.Application.Authentication.Commands.CreateToken
{
    public class CreateTokenCommandHandler(
        IUsersRepository _usersRepository,
        IUnitOfWork _unitOfWork,
        ITokenGenerator _tokenGenerator,
        IUserTokensRepository _tokenRepsitory,
        IEmailService _emailService)
        : IRequestHandler<CreateTokenCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {


            var user = await _usersRepository.GetByEmailAsync(request.Email);

            if (user is null)
            {
                return Error.Custom(code: "NotFound", description: "No, Account With this Email.", type: 4);
            }

            TokenTypes tokenType;

            try
            {
                tokenType = TokenTypes.FromName(request.Type, ignoreCase: true);
            }
            catch (Exception)
            {
                return Error.Custom(code: "InvalidTokenType", description: "Invalid Token Type.", type: 4);
            }
            

            var token =  _tokenGenerator.GenerateUserTokens(user,tokenType);

            await _emailService.SendEmailAsync(
                request.Email,
                $"Your {request.Type} Token From HealLink",
                EmailBodyTemplates.GenerateTemplate(user.NameToShow, token.Token,token.Type)
            );
            await _tokenRepsitory.AddAsync(token);
            await _unitOfWork.CommitChangesAsync();

            return new Success();

        }
    }
}
