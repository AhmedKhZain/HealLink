using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.MedicalHistories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Patients.Commands.AddMedicalHistory
{
    public class AddMedicalHistoryCommandHandler
        (IMedicalHistoryRepository _medicalHistoryRepsitory,
         IUnitOfWork _UnitOfWork,
         IPatientRepository _patientRepository)
        : IRequestHandler<AddMedicalHistoryCommand, ErrorOr<AddMedicalHistoryCommandResponse>>
    {
        public async Task<ErrorOr<AddMedicalHistoryCommandResponse>> Handle(AddMedicalHistoryCommand request, CancellationToken cancellationToken)
        {

            
            var patient = await _patientRepository.GetPatientByUserIdAsync(request.PatientId);
            if (patient is null)
            {
                return Error.NotFound("Patient", "Patient not found.");
            }
            var medicalHistory = new MedicalHistory(
                request.PatientId,
                MedicalHistoryType.FromName(request.Type,true),
                request.Description,
                request.FileLink);
            var ruselt = await _UnitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _medicalHistoryRepsitory.AddMedicalHistoryAsync(medicalHistory);

            });
            if (ruselt.IsError)
            {
                return ruselt.Errors;
            }
            return new AddMedicalHistoryCommandResponse(medicalHistory.Id);
        }
    }
}
