using DeveloperExam.Domain.Abstractions;
using Moq;
using DeveloperExam.Application.UserProfiles.Commands.CreateUserProfile;
using User = DeveloperExam.Domain.Entities.UserProfile;
using Shouldly;

namespace DeveloperExam.Application.Tests.UserProfile.Commands
{
    public class CreateUserProfileCommandHandlerTest
    {
        [Fact]
        public void Handle_Should_ReturnSuccessResult_WhenUserProfileIsCreated()
        {
            // Arrange
            var userProfileRepository = new Mock<IUserProfileRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var handler = new CreateUserProfileCommandHandler(userProfileRepository.Object, unitOfWork.Object);

            userProfileRepository.Setup(x => x.AddAsync(It.IsAny<User>()));
            unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            
            var validator = new CreateUserProfileCommandValidator();
            var command = new CreateUserProfileCommand("John Doe", 70, 170, new DateOnly(1992, 08, 03));

            // Act
            var validate = validator.Validate(command);
            var result = handler.Handle(command, CancellationToken.None).Result;

            // Assert
            validate.IsValid.ShouldBeTrue();
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_WhenUserProfileValidatorsFail()
        {
            // Arrange
            var userProfileRepository = new Mock<IUserProfileRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var handler = new CreateUserProfileCommandHandler(userProfileRepository.Object, unitOfWork.Object);

            userProfileRepository.Setup(x => x.AddAsync(It.IsAny<User>()));
            unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var validator = new CreateUserProfileCommandValidator();
            var command = new CreateUserProfileCommand("", 0, 0, new DateOnly(2050, 08, 03));

            // Act
            var validate = validator.Validate(command);
            var result = handler.Handle(command, CancellationToken.None).Result;

            // Assert
            validate.IsValid.ShouldBeFalse();
            validate.IsValid.ShouldBeFalse();
            result.Success.ShouldBeFalse();
            validate.Errors.Count.ShouldBe(6);
            validate.Errors[0].ErrorMessage.ShouldBe("Name is required.");
            validate.Errors[1].ErrorMessage.ShouldBe("Weight is required.");
            validate.Errors[2].ErrorMessage.ShouldBe("Weight must be greater than 0.");
            validate.Errors[3].ErrorMessage.ShouldBe("Height is required.");
            validate.Errors[4].ErrorMessage.ShouldBe("Height must be greater than 0.");
            validate.Errors[5].ErrorMessage.ShouldBe("Birth date must be less than current date.");
        }
    }
}
