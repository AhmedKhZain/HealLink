using ErrorOr;
using FluentValidation;
using HealLink.Application.Common.Authorization;
using HealLink.Domain.MedicalHistories;
using HealLink.Domain.Patients.HealLink.Domain.Patients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Patients.Queries.GetMedicalHistory;
[Authorize(Role = "Patient")]
public record GetMedicalHistoryQuery
    (Guid UserId,
    string Type,
    int PageNumber = 1,
    int PageSize = 12):IRequest<ErrorOr<IEnumerable<MedicalHistory>>>;


public class GetMedicalHistoryQueryValidator : AbstractValidator<GetMedicalHistoryQuery>
{

    private readonly IReadOnlyList<string> ValidTypes = new List<string>
        {
            "MedicalExaminations",
            "Allergy",
            "ChronicDisease",
            "Medication"
        };
    public GetMedicalHistoryQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required.")
            .Must(type => ValidTypes.Contains(type) )
            .WithMessage("Invalid medical history type.");
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");
        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}
