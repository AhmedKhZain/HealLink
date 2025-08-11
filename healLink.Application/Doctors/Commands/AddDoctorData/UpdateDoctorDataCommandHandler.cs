using ErrorOr;
using healLink.Application.Common.Interfaces.Repositories;
using healLink.Application.Common.Interfaces.Service;
using MediatR;

namespace healLink.Application.Doctors.Commands.AddDoctorData;

public class UpdateDoctorDataCommandHandler 
    (IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork): IRequestHandler<UpdateDoctorDataCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateDoctorDataCommand request, CancellationToken cancellationToken)
    {
        var doctor =await doctorRepository.GetDoctorByIdAsync(request.DoctorId);
        if (doctor is null)
            return Error.NotFound(description:"No doctor with the sended Id.");

        doctor.UpdateDoctorData(
            request.syndicateIdImageLink,
            request.nationalId,
            request.licenseNumber);

        try
        {
            doctorRepository.UpdateDoctor(doctor);
            unitOfWork.CommitChangesAsync();
        }
        catch (Exception ex)
        {
            return Error.Failure(description: "An Exception throwed while Updating the database." + ex.Message);
        }

        return Result.Success;
    }
}
