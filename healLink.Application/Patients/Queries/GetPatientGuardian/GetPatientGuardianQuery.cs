using ErrorOr;
using healLink.Application.Patients.Common;
using HealLink.Application.Common.Authorization;
using MediatR;

namespace healLink.Application.Patients.Queries.GetPatientGuardian;

[Authorize(Role = "Patient")]
public record GetPatientGuardianQuery
    (Guid Id):IRequest<ErrorOr<PatientGuardianResponse>>;
