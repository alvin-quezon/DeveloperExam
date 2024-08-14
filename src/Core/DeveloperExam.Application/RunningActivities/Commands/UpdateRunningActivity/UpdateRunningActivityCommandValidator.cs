using FluentValidation;

namespace DeveloperExam.Application.RunningActivities.Commands.UpdateRunningActivity;

public sealed class UpdateRunningActivityCommandValidator : AbstractValidator<UpdateRunningActivityCommand>
{
    public UpdateRunningActivityCommandValidator()
    {
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required");

        RuleFor(x => x.End)
            .NotEmpty().WithMessage("End is required");
        
        RuleFor(x => x.Start)
            .NotEmpty().WithMessage("Start is required")
            .GreaterThan(x => x.Start).WithMessage("End must be greater than Start");
        
        RuleFor(x => x.Distance)
            .NotEmpty().WithMessage("Distance is required")
            .GreaterThan(0).WithMessage("Distance must be greater than 0");
    }
}