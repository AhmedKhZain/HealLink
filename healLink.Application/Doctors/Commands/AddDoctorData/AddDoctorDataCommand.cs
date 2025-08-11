using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Doctors.Commands.AddDoctorData;

public record UpdateDoctorDataCommand(
    Guid DoctorId,
    string? syndicateIdImageLink,
    string? nationalId,
    string? licenseNumber):IRequest<ErrorOr<Success>>;
