using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Patients.HealLink.Domain.Patients;
using MediatR;

namespace healLink.Application.Patients.Commands.AddGuardian
{
    public class AddGuardianCommandHandler(
        IPatientGuardianRepository guardianRepository,
        IUnitOfWork unitOfWork,
        IPatientRepository patientRepository) 
        : IRequestHandler<AddGuardianCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(AddGuardianCommand command, CancellationToken cancellationToken)
        {
            var patient = await patientRepository.GetPatientByUserIdAsync(command.patientId);
            var guardian = await patientRepository.GetPatientByUserIdAsync(command.guardianId);
            if (patient is null || guardian is null)
            {
                return Error.NotFound("Patient or Guardian not found.");
            }

            var relationship = new PatientGuardian(patient, guardian);


            var result =await unitOfWork.ExecuteInTransactionAsync(async ()=>
            {

                await guardianRepository.AddPatientGuardianAsync(relationship);
                if (command.relation is not null)
                    relationship.AddRelationship(command.relation);

            });
            if (result.IsError)
            {
                return result.Errors;
            }
            return Result.Success;

        }
    }
}
