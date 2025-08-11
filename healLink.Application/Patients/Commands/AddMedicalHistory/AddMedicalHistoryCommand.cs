using ErrorOr;
using FluentValidation;
using HealLink.Application.Common.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Patients.Commands.AddMedicalHistory;

[Authorize(Role ="Patient")]
public record AddMedicalHistoryCommand(
    Guid PatientId,
    string FileLink,
    string Description,
    string Type
) : IRequest<ErrorOr<AddMedicalHistoryCommandResponse>>
{
    public class AddMedicalHistoryCommandValidator : AbstractValidator<AddMedicalHistoryCommand>
    {
        private readonly IReadOnlyList<string> ValidTypes = new List<string>
        {
            "MedicalExaminations",
            "Allergy",
            "ChronicDisease",
            "Medication"
        };
        public AddMedicalHistoryCommandValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("Patient ID is required.");
            RuleFor(x => x.FileLink).NotEmpty().WithMessage("File link is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.")
                .Must(type => ValidTypes.Contains(type)).WithMessage("Invalid medical history type.");

        }
    }
}
