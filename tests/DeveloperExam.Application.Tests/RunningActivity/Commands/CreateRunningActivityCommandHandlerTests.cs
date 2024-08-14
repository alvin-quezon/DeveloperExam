using DeveloperExam.Application.RunningActivities.Commands.CreateRunningActivity;
using DeveloperExam.Domain.Abstractions;
using Moq;
using Shouldly;
using Activity = DeveloperExam.Domain.Entities.RunningActivity;
using User = DeveloperExam.Domain.Entities.UserProfile;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.Tests.RunningActivity.Commands;

public class CreateRunningActivityCommandHandlerTests
{
    [Fact]
    public void Handle_Should_ReturnsSuccessResult_WhenRunningActivityIsCreated()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();
        var userProfileRepository = new Mock<IUserProfileRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateRunningActivityCommandHandler(runningActivityRepository.Object, userProfileRepository.Object, unitOfWork.Object);

        userProfileRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Domain.Entities.UserProfile(It.IsAny<Guid>(), "Jose Rizal", 180, 70, DateTime.Now.AddYears(-20)));
        runningActivityRepository.Setup(x => x.AddAsync(It.IsAny<Activity>()));
        unitOfWork.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


        var validator = new CreateRunningActivityValidator();
        var command = new CreateRunningActivityCommand(Guid.NewGuid(), "Mandaluyong City", DateTime.Now, DateTime.Now.AddMinutes(280), 13);

        // Act
        var validate = validator.Validate(command);
        var result = handler.Handle(command, CancellationToken.None).Result;

        // Assert
        validate.IsValid.ShouldBeTrue();
        result.ShouldBeOfType<ServiceResponse>();
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public void Handle_Should_ReturnsErrorResult_WhenRunningActivityIsNotCreated()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();
        var userProfileRepository = new Mock<IUserProfileRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new CreateRunningActivityCommandHandler(runningActivityRepository.Object, userProfileRepository.Object, unitOfWork.Object);

        userProfileRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((User)null);
        runningActivityRepository.Setup(x => x.AddAsync(It.IsAny<Activity>()));
        unitOfWork.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var validator = new CreateRunningActivityValidator();
        var command = new CreateRunningActivityCommand(It.IsAny<Guid>(), "", DateTime.Now.AddMinutes(280), DateTime.Now.AddMinutes(-60), 0);

        // Act
        var validate = validator.Validate(command);
        Func<Task> act = async() => await handler.Handle(command, CancellationToken.None);

        // Assert
        validate.IsValid.ShouldBeFalse();
        validate.Errors.Count.ShouldBe(5);
        validate.Errors[0].ErrorMessage.ShouldBe("User profile id is required.");
        validate.Errors[1].ErrorMessage.ShouldBe("Location is required.");
        validate.Errors[2].ErrorMessage.ShouldBe("End date must be greater than start date.");
        validate.Errors[3].ErrorMessage.ShouldBe("Distance must be greater than 0.");
        validate.Errors[4].ErrorMessage.ShouldBe("Distance is required.");
        var exception = act.ShouldThrowAsync<UserProfileNotFoundException>();
        exception.Result.Message.ShouldContain("was not found");
    }
}
