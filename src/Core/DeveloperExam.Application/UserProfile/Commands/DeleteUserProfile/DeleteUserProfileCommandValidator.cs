using FluentValidation;

namespace DeveloperExam.Application.UserProfile.Commands.DeleteUserProfile;

public sealed class DeleteUserProfileCommandValidator : AbstractValidator<DeleteUserProfileCommand>
{
    public DeleteUserProfileCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}