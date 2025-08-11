using ErrorOr;
using FluentValidation;
using HealLink.Application.Common.Authorization;
using MediatR;

namespace healLink.Application.Patients.Commands.AddGuardian;
[Authorize(Role ="Patient")]
public record AddGuardianCommand
    (Guid patientId,
    Guid guardianId,
    string? relation)
    :IRequest<ErrorOr<Success>>;

public class AddGuardianVlidator : AbstractValidator<AddGuardianCommand>
{
    public AddGuardianVlidator()
    {
        RuleFor(x => x.patientId)
            .NotEmpty().WithMessage("Patient ID cannot be empty.");
        RuleFor(x => x.patientId)
            .NotEqual(Guid.Empty).WithMessage("Patient ID cannot be an empty GUID.");

        RuleFor(x => x.guardianId)
            .NotEmpty().WithMessage("Guardian ID cannot be empty.");
        
        RuleFor(x => x.relation)
            .NotEmpty().WithMessage("Relationship cannot be empty.")
            .MaximumLength(100).WithMessage("Relationship must not exceed 100 characters.");
    }
}
