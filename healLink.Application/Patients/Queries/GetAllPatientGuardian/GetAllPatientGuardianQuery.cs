using ErrorOr;
using healLink.Application.Patients.Common;
using HealLink.Application.Common.Authorization;
using MediatR;

namespace healLink.Application.Patients.Queries.GetAllPatientGuardian;
[Authorize(Role = "Patient")]

public record GetAllPatientGuardianQuery
    (Guid UserId,
    int PageNumber = 1,
    int PageSize = 12,
    bool NewistFirst= true):IRequest<ErrorOr<IEnumerable<PatientGuardianResponse>>>;


