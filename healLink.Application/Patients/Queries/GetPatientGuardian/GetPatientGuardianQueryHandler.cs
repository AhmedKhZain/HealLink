using ErrorOr;

using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Patients.Common;
using MediatR;

namespace healLink.Application.Patients.Queries.GetPatientGuardian;

public class GetPatientGuardianQueryHandler
    (IPatientGuardianRepository patientGuardianRepository) 
    : IRequestHandler<GetPatientGuardianQuery, ErrorOr<PatientGuardianResponse>>
{

    public async Task<ErrorOr<PatientGuardianResponse>> Handle(GetPatientGuardianQuery request, CancellationToken cancellationToken)
    {
        var p = await patientGuardianRepository.GetPatientGuardianByIdAsync(request.Id,false);


        if (p is null)
        {
            return Error.NotFound("No guardians found for the specified patient.");
        }


        return new PatientGuardianResponse
        {
            Id = p.Id,
            GuardianId = p.GuardianId,
            GuardianName = p.Guardian.User.NameToShow,
            GuardianPhotoLink = p.Guardian.User.ProfilePhotoLink,
            PatientId = p.PatientId,
            PatientName = p.Patient.User.NameToShow,
            PatientPhotoLink = p.Patient.User.ProfilePhotoLink,
            Relationship = p.RelationshipType ?? "",
            StartedAt = p.CreatedAt
        };
    }
}