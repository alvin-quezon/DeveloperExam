using FluentValidation;

namespace DeveloperExam.Application.UserProfile.Commands.UpdateUserProfile;

public sealed class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Weight)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Height)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.BirthDate)
            .NotEmpty();
    }
}