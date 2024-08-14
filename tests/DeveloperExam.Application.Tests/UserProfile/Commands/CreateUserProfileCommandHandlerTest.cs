using DeveloperExam.Domain.Abstractions;
using Moq;
using DeveloperExam.Application.UserProfiles.Commands.CreateUserProfile;
using FluentValidation;
using FluentValidation.Results;

namespace DeveloperExam.Application.Tests.UserProfile.Commands
{
    public class CreateUserProfileCommandHandlerTest
    {
        [Fact]
        public void Handle_Should_ReturnSuccessResult_WhenUserProfileIsCreated()
        {
            // Arrange
            var validator = new CreateUserProfileCommandValidator();
            var command = new CreateUserProfileCommand("John Doe", 70, 170, new DateOnly(1992, 08, 03));

            // Act
            var validate = validator.Validate(command);

            // Assert
            Assert.True(validate.IsValid);
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_WhenUserProfileValidatorsFail()
        {
            // Arrange
            var validator = new CreateUserProfileCommandValidator();
            var command = new CreateUserProfileCommand("", 0, 0, new DateOnly(2050, 08, 03));

            // Act
            var validate = validator.Validate(command);

            // Assert
            Assert.False(validate.IsValid);
            Assert.Equal(6, validate.Errors.Count);
            Assert.Equal("Name is required.", validate.Errors[0].ErrorMessage);
            Assert.Equal("Weight is required.", validate.Errors[1].ErrorMessage);
            Assert.Equal("Weight must be greater than 0.", validate.Errors[2].ErrorMessage);
            Assert.Equal("Height is required.", validate.Errors[3].ErrorMessage);
            Assert.Equal("Height must be greater than 0.", validate.Errors[4].ErrorMessage);
            Assert.Equal("Birth date must be less than current date.", validate.Errors[5].ErrorMessage);
        }
    }
}
