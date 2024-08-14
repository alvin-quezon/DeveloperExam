using DeveloperExam.Application.Dto;
using DeveloperExam.Application.RunningActivities.Commands.UpdateRunningActivity;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;
using Moq;
using Shouldly;
using Activity = DeveloperExam.Domain.Entities.RunningActivity;

namespace DeveloperExam.Application.Tests.RunningActivity.Commands;

public class UpdateRunningActivityCommandHandlerTests
{
    [Fact]
    public void Handle_Should_UpdateRunningActivity()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var handler = new UpdateRunningActivityCommandHandler(runningActivityRepository.Object, unitOfWork.Object);

        runningActivityRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Activity(It.IsAny<Guid>(), It.IsAny<Guid>(), "Makati City", DateTime.Now, DateTime.Now.AddMinutes(220), 10));

        runningActivityRepository.Setup(x => x.Update(It.IsAny<Activity>()));
        unitOfWork.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var validator = new UpdateRunningActivityCommandValidator();
        var command = new UpdateRunningActivityCommand(It.IsAny<Guid>(), "Mandaluyong City", DateTime.Now, DateTime.Now.AddMinutes(280), 13);
        // Act
        var validate = validator.Validate(command);
        var result = handler.Handle(command, CancellationToken.None)?.Result;

        // Assert
        validate.IsValid.ShouldBeTrue();
        result.ShouldBeOfType<ServiceResponse>();
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public void Handle_Should_ReturnError_WhenRunningActivityIsNotUpdated()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var handler = new UpdateRunningActivityCommandHandler(runningActivityRepository.Object, unitOfWork.Object);

        runningActivityRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Activity)null);

        runningActivityRepository.Setup(x => x.Update(It.IsAny<Activity>()));
        unitOfWork.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var validator = new UpdateRunningActivityCommandValidator();
        var command = new UpdateRunningActivityCommand(It.IsAny<Guid>(), string.Empty, DateTime.Now.AddMinutes(100), DateTime.Now.AddMinutes(-280), 0);

        // Act
        var validate = validator.Validate(command);
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        validate.IsValid.ShouldBeFalse();
        validate.Errors.Count.ShouldBe(4);
        validate.Errors[0].ErrorMessage.ShouldBe("Location is required");
        validate.Errors[1].ErrorMessage.ShouldBe("End must be greater than Start");
        validate.Errors[2].ErrorMessage.ShouldBe("Distance is required");
        validate.Errors[3].ErrorMessage.ShouldBe("Distance must be greater than 0");
        var exception = act.ShouldThrowAsync<RunningActivityNotFoundException>();
        exception.Result.Message.ShouldContain("was not found");
    }
}
