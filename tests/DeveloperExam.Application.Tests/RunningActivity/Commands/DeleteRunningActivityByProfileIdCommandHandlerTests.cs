using DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivitiesByProfileId;
using DeveloperExam.Domain.Abstractions;
using Moq;
using Shouldly;
using Activity = DeveloperExam.Domain.Entities.RunningActivity;
using User = DeveloperExam.Domain.Entities.UserProfile;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.Tests.RunningActivity.Commands;

public class DeleteRunningActivityByProfileIdCommandHandlerTests
{
    [Fact]
    public void Handle_Should_ReturnsSuccessResult_WhenRunningActivityIsDeleted()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();
        var userProfileRepository = new Mock<IUserProfileRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new DeleteRunningActivityByProfileIdCommandHandler(runningActivityRepository.Object, userProfileRepository.Object, unitOfWork.Object);

        userProfileRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new User(It.IsAny<Guid>(), "Jose Rizal", 180, 70, DateTime.Now.AddYears(-20)));
        runningActivityRepository.Setup(x => x.Delete(It.IsAny<Activity>()));
        unitOfWork.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var validator = new DeleteRunningActivityByProfileIdCommandValidator();
        var command = new DeleteRunningActivitiesByProfileIdCommand(Guid.NewGuid());

        // Act
        var validate = validator.Validate(command);
        var result = handler.Handle(command, CancellationToken.None).Result;

        // Assert
        validate.IsValid.ShouldBeTrue();
        result.ShouldBeOfType<ServiceResponse>();
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public void Handle_Should_ReturnsErrorResult_WhenRunningActivityIsNotDeleted()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();
        var userProfileRepository = new Mock<IUserProfileRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new DeleteRunningActivityByProfileIdCommandHandler(runningActivityRepository.Object, userProfileRepository.Object, unitOfWork.Object);

        userProfileRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((User)null);
        runningActivityRepository.Setup(x => x.Delete(It.IsAny<Activity>()));
        unitOfWork.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var validator = new DeleteRunningActivityByProfileIdCommandValidator();
        var command = new DeleteRunningActivitiesByProfileIdCommand(It.IsAny<Guid>());

        // Act
        var validate = validator.Validate(command);
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        validate.IsValid.ShouldBeFalse();
        var exception = act.ShouldThrowAsync<UserProfileNotFoundException>();
        exception.Result.Message.ShouldContain("was not found");
    }
}
