using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Patients.Common;
using MediatR;

namespace healLink.Application.Patients.Queries.GetAllPatientGuardian;

public class GetAllPatientGuardianQueryHandler 
    (IPatientGuardianRepository patientGuardianRepository)
    : IRequestHandler<GetAllPatientGuardianQuery, ErrorOr<IEnumerable<PatientGuardianResponse>>>
{

    public async Task<ErrorOr<IEnumerable<PatientGuardianResponse>>> Handle(GetAllPatientGuardianQuery request, CancellationToken cancellationToken)
    {
        var patientGuardians = await patientGuardianRepository
            .GetPatientGuardianResponsesByUserIdAsync(request.UserId, request.PageNumber, request.PageSize,request.NewistFirst);
        if (patientGuardians is null || !patientGuardians.Any())
            return Error.NotFound("PatientGuardians.NotFound",
                "No patient guardians found for the specified user.");


        return patientGuardians.ToErrorOr<IEnumerable<PatientGuardianResponse>>();
    }
}


