using FluentValidation;

namespace DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivityById;

public sealed class DeleteRunningActivityByIdCommandValidator : AbstractValidator<DeleteRunningActivityByIdCommand>
{
    public DeleteRunningActivityByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}