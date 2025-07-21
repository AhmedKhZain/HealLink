using ErrorOr;
using healLink.Application.Common.Interfaces;
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
         IUserTokensRepository _refreshTokenRepository)
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
            if (token.IsUsed)
                return Error.Validation(
                    code: "RefreshToken",
                    description: "Refresh token is already used");


            var authToken = _tokenGenerator.GenerateJwtToken(token.User);
            token.MarkUsed();
             
            return new AuthenticationResult(
                Token: authToken,
                User: token.User);  

        }
    }
}
