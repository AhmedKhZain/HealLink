using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using HealLink.Domain.Requests;
using MediatR;

namespace healLink.Application.Requests.Queries.GetRequestsByDoctorId;

public class GetRequestsByDoctorIdQueryHandler 
    (IDoctorRequestRepository doctorRequestRepository)
    : IRequestHandler<GetRequestsByDoctorIdQuery, ErrorOr<IEnumerable<DoctorRequest>>>
{

    public async Task<ErrorOr<IEnumerable<DoctorRequest>>> Handle(GetRequestsByDoctorIdQuery request, CancellationToken cancellationToken)
    {
        var requests = await doctorRequestRepository.GetDoctorRequestsByDoctorIdAsync(request.DoctorId);
        if (requests is null || !requests.Any())
        {
            return Error.NotFound("No requests found for the specified doctor.");
        }
        return requests.ToErrorOr();
    }
}