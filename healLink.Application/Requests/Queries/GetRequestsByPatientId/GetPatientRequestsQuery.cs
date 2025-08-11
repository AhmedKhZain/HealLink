using ErrorOr;
using FluentValidation;
using HealLink.Application.Common.Authorization;
using HealLink.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Requests.Queries.GetRequestsByPatientId;

[Authorize(Role = "Patient")]

public record GetPatientRequestsQuery
    (Guid PatientId,
    bool newestFirst = true):IRequest<ErrorOr<IEnumerable<DoctorRequest>>>;


public class GetPatientRequestsQueryValidator : AbstractValidator<GetPatientRequestsQuery>
{
    public GetPatientRequestsQueryValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty().WithMessage("Patient ID is required.");

    }
}
