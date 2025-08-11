using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Doctors;
using HealLink.Domain.Users;
using MediatR;

namespace healLink.Application.Admins.Commands.ChangeTheRole;

public class ChangeRoleToDoctorCommandhandler(
    IDoctorRepository doctorRepository,
    IPatientRepository patientRepository,
    ICurrentUserProvider userProvider,
    IUnitOfWork unitOfWork,
    IUsersRepository usersRepository) : IRequestHandler<ChangeRoleToDoctorCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(ChangeRoleToDoctorCommand request, CancellationToken cancellationToken)
    {



        var Patient = await patientRepository.GetPatientByUserIdAsync(request.UserId,true);
        var User = await usersRepository.GetByIdAsync(request.UserId);
        var AdminId = userProvider.GetCurrentUser().Value.Id;

        if (User is null || Patient is null)
            return Error.NotFound(description: "The user is not found or his role is changed already");

        var Doctor = new Doctor(request.UserId);
        User.ChangeRole(Role.Doctor);


        var result = await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            patientRepository.DeletePatient(Patient);
            usersRepository.Update(User);
            await doctorRepository.AddDoctorAsync(Doctor);
        });

        if (result.IsError)
            return Error.Failure(description: "Failure on updating the database");

        User.NullThePassword();
        return User;


    }
}

