using ErrorOr;
using MediatR;

namespace healLink.Application.Doctors.Commands.AddDoctorData;

public record UpdateDoctorDataCommand(
    Guid DoctorId,
    string? syndicateIdImageLink,
    string? nationalId,
    string? licenseNumber):IRequest<ErrorOr<Success>>;
