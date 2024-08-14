using FluentValidation;

namespace DeveloperExam.Application.UserProfiles.Commands.CreateUserProfile;

public sealed class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>
{
    public CreateUserProfileCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Weight is required.")
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");

        RuleFor(x => x.Height)
            .NotEmpty().WithMessage("Height is required.")
            .GreaterThan(0).WithMessage("Height must be greater than 0.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required.")
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Birth date must be less than current date.");
    }
}
