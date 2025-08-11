using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using MediatR;

namespace healLink.Application.Patients.Commands.DeleteGuardian;

public class DeleteGuardianCommandHandler
    (IUnitOfWork unitOfWork,
    IPatientGuardianRepository guardianRepository) : IRequestHandler<DeleteGuardianCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteGuardianCommand request, CancellationToken cancellationToken)
    {
        var patientGuardian = await guardianRepository.GetPatientGuardianByIdAsync(request.patientguardianId, true);

        if (patientGuardian is null)
        {
            return Error.NotFound("PatientGuardian not found");
        }

        var ruselt = await unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            guardianRepository.DeletePatientGuardian(patientGuardian);
            await Task.CompletedTask;
        });

        return new Success();
    }
}
