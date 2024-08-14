using FluentValidation;

namespace DeveloperExam.Application.RunningActivities.Commands.CreateRunningActivity;

public sealed class CreateRunningActivityValidator : AbstractValidator<CreateRunningActivityCommand>
{
    public CreateRunningActivityValidator()
    {
        RuleFor(x => x.UserProfileId)
            .NotEmpty().WithMessage("User profile id is required.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.");

        RuleFor(x => x.Start)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.End)
            .GreaterThan(x => x.Start).WithMessage("End date must be greater than start date.")
            .NotEmpty().WithMessage("End date is required.");

        RuleFor(x => x.Distance)
            .GreaterThan(0).WithMessage("Distance must be greater than 0.")
            .NotEmpty().WithMessage("Distance is required.");
    }
}