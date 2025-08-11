using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using MediatR;

namespace healLink.Application.Admins.Commands.ApproveDoctor;

public class ApproveDoctorCommandHandler (
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserProvider userProvider): IRequestHandler<ApproveDoctorCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ApproveDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor =await doctorRepository.GetDoctorByIdAsync(request.DoctorId,true);

        if (doctor is null)
            return Error.NotFound("The Doctor Is Not applayed As a doctor.");

        var AdminId = userProvider.GetCurrentUser().Value.Id;
        var result =doctor.Approve(AdminId);

        return result;

    }
}
