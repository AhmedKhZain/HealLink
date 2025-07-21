using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator:
        AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Token)
                .NotEmpty()
                .MaximumLength(6).MinimumLength(6);
        }
    }
}
