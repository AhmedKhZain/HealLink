using ErrorOr;
using MediatR;
using HealLink.Application.Authentication.Common;
using HealLink.Domain.Users;
using HealLink.Domain.Common;
using HealLink.Domain.Patients;
using healLink.Application.Common.Interfaces.Service;
using healLink.Application.Common.Interfaces.Repositories;

namespace HealLink.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    ITokenGenerator _tokenGenerator,
    IPasswordHasher _passwordHasher,
    IUsersRepository _usersRepository,
    IPatientRepository _iPationtRepsitory,
    IUnitOfWork _unitOfWork)
        : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await _usersRepository.ExistsByEmailAsync(command.Email))
        {
            return Error.Conflict(description: "User already exists");
        }

        var hashPasswordResult = _passwordHasher.HashPassword(command.Password);

        if (hashPasswordResult.IsError)
        {
            return hashPasswordResult.Errors;
        }

        var user = new User(
            command.FullName,
            command.ShowName,
            command.Email,
            hashPasswordResult.Value);



        var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            await _usersRepository.AddUserAsync(user);
            await _iPationtRepsitory.AddPatientAsync(new Patient(user.Id));
        });

        if (result.IsError)
            return result.Errors;

        var token = _tokenGenerator.GenerateJwtToken(user);

        return new AuthenticationResult(user, token);
    }
}