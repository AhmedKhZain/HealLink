using FluentValidation;

namespace healLink.Application.Doctors.Commands.AddDoctorData;

public class AddDoctorCommandValidator :AbstractValidator<UpdateDoctorDataCommand>
{
    public AddDoctorCommandValidator()
    {
        RuleFor(c => c).Must(c =>
        (c.licenseNumber is not null) 
        || (c.nationalId is not null) ||
        (c.syndicateIdImageLink is not null))
            .WithMessage("Cant Send Empty Command To the Handler.");
    }
}

