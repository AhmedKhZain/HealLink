using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.MedicalHistories;
using MediatR;

namespace healLink.Application.Patients.Queries.GetMedicalHistory;

public class GetMedicalHistoryQueryHandler(
    IMedicalHistoryRepository _historyRepository) : IRequestHandler<GetMedicalHistoryQuery, ErrorOr<IEnumerable<MedicalHistory>>>
{
    public async Task<ErrorOr<IEnumerable<MedicalHistory>>> Handle(GetMedicalHistoryQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<MedicalHistory> histories = await _historyRepository.GetMedicalHistoriesByUserIdAsync(
            request.UserId,
            MedicalHistoryType.FromName(request.Type),
            request.PageNumber,
            request.PageSize);

        if (!histories.Any())
        {
            return Error.NotFound("MedicalHistory", "No medical history found for the given criteria.");
        }

        return histories.ToList();



    }
}
