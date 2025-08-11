using ErrorOr;
using FluentValidation;
using HealLink.Application.Common.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Requests.Commands.CancelPatientRequest;
[Authorize(Role = "Patient")]
public record CancelRequestCommand
    (
    Guid RequestId
) : IRequest<ErrorOr<Success>>
{
    public class CancelRequestCommandValidator : AbstractValidator<CancelRequestCommand>
    {
        public CancelRequestCommandValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("Request ID is required.");
        }
    }
}
