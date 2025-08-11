using ErrorOr;
using MediatR;

namespace healLink.Application.Admins.Commands.ApproveDoctor;

public record ApproveDoctorCommand(Guid DoctorId):IRequest<ErrorOr<Success>>;
