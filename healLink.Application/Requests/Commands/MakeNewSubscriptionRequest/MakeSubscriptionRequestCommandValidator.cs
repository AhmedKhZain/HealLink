using FluentValidation;

namespace healLink.Application.Requests.Commands.MakeNewSubscriptionRequest;

public class MakeSubscriptionRequestCommandValidator:AbstractValidator<MakeSubscriptionRequestCommand>
{

    private readonly IReadOnlyList<string> ValidPlans = new List<string>
    {
        "Monthly",
        "Weekly",
        "Daily"
    };
    private readonly IReadOnlyList<string?> ValidTypes = new List<string?>
    {
        null,
        "Renewal",
        "Initial"
    };
    public MakeSubscriptionRequestCommandValidator()
    {
        RuleFor(c => c.plan).NotEmpty().WithMessage("plan is required")
            .Must(plan => ValidPlans.Contains(plan)).WithMessage("invalid Plan");
        RuleFor(c => c.PatientId).NotEmpty().WithMessage("PatientId is required");
        RuleFor(c => c.DoctorId).NotEmpty().WithMessage("DoctorId is required");
        RuleFor(c => c.RequestTypename).Must(type => ValidTypes.Contains(type));

        RuleFor(c => c.subscriptionId)
            .NotEmpty()
            .When(c => c.RequestTypename == "Renewal")
            .WithMessage("Subscription is required when RequestTypename is Renewal");
    }


}


