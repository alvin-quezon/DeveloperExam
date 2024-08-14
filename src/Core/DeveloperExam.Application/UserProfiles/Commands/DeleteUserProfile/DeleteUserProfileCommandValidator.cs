using FluentValidation;

namespace DeveloperExam.Application.UserProfiles.Commands.DeleteUserProfile;

public sealed class DeleteUserProfileCommandValidator : AbstractValidator<DeleteUserProfileCommand>
{
    public DeleteUserProfileCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}