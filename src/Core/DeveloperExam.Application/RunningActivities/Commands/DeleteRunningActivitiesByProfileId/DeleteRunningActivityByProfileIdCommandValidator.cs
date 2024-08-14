using FluentValidation;

namespace DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivitiesByProfileId;

public sealed class DeleteRunningActivityByProfileIdCommandValidator : AbstractValidator<DeleteRunningActivitiesByProfileIdCommand>
{
    public DeleteRunningActivityByProfileIdCommandValidator()
    {
        RuleFor(x => x.UserProfileId).NotEmpty();
    }
}
