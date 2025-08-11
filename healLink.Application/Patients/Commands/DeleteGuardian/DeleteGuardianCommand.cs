using ErrorOr;
using HealLink.Application.Common.Authorization;
using MediatR;

namespace healLink.Application.Patients.Commands.DeleteGuardian;

[Authorize(Role = "Patient")]
public record DeleteGuardianCommand
    (Guid patientguardianId)
    :IRequest<ErrorOr<Success>>;
