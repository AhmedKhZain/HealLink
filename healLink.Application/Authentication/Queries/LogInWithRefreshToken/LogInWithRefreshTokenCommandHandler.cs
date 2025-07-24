using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Application.Authentication.Common;
using HealLink.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Authentication.Queries.LogInWithRefreshToken
{
    public class LogInWithRefreshTokenQueryHandler
        (ITokenGenerator _tokenGenerator,
         IUserTokensRepository _refreshTokenRepository,
         IUnitOfWork _unitOfWork,
         IUserTokensRepository _tokensRepository)
        : IRequestHandler<LogInWithRefreshTokenQuery, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(LogInWithRefreshTokenQuery request, CancellationToken cancellationToken)
        {

            var token =  await _refreshTokenRepository
                .GetByTokenAndTypeAsyncAndUserId(request.RefreshToken, TokenTypes.RefreshToken,request.UserId);

            if (token is null)
                return Error.NotFound();
            
            if (token.IsExpired())
                return Error.Validation(
                    code: "RefreshToken",
                    description: "Refresh token is expired");



            var authToken = _tokenGenerator.GenerateJwtToken(token.User);
            token.MarkUsed();
            var result = await _unitOfWork.ExecuteInTransactionAsync(() =>
            {
                _tokensRepository.Update(token);
                return Task.CompletedTask;
            });
            if (result.IsError)
                return result.Errors;
             
            return new AuthenticationResult(
                Token: authToken,
                User: token.User);  

        }
    }
}
