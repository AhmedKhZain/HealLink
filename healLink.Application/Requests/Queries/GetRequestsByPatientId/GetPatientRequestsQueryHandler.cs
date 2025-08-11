using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using HealLink.Domain.Requests;
using MediatR;
using Stripe.BillingPortal;

namespace healLink.Application.Requests.Queries.GetRequestsByPatientId;

public class GetPatientRequestsQueryHandler
    (IDoctorRequestRepository doctorRequestRepository) 
    : IRequestHandler<GetPatientRequestsQuery, ErrorOr<IEnumerable<DoctorRequest>>>
{

    public async Task<ErrorOr<IEnumerable<DoctorRequest>>> Handle(GetPatientRequestsQuery request, CancellationToken cancellationToken)
    {
        

        var requests = await doctorRequestRepository
            .GetDoctorRequestsByPatientIdAsync(request.PatientId,request.newestFirst);

        if (requests is null || !requests.Any())
        {
            return Error.NotFound("No requests found for this patient.");
        }
        


        return requests.ToErrorOr();


    }
}