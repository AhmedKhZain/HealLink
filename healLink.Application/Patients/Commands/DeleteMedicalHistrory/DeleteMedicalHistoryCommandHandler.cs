using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Patients.Commands.DeleteMedicalHistrory;

public class DeleteMedicalHistoryCommandHandler (
    IMedicalHistoryRepository _historyRepository,
    IUnitOfWork _unitOfWork): IRequestHandler<DeleteMedicalHistoryCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(DeleteMedicalHistoryCommand request, CancellationToken cancellationToken)
    {

        var medicalHistory = await _historyRepository.GetMedicalHistoryByIdAsync(request.MedicalHistoryId, true);
        if (medicalHistory is null)
        {
            return Error.NotFound("MedicalHistory", "Medical history not found.");
        }

        var ruselt =await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            _historyRepository.DeleteMedicalHistory(medicalHistory);
            await Task.CompletedTask;
        });
        
        if (ruselt.IsError)
        {
            return ruselt.Errors;
        }
        return Result.Success;
    }
}
