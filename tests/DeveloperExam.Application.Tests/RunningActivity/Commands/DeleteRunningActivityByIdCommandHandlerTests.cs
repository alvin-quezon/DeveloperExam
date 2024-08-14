using DeveloperExam.Application.Dto;
using DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivityById;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;
using Moq;
using Shouldly;
using Activity = DeveloperExam.Domain.Entities.RunningActivity;

namespace DeveloperExam.Application.Tests.RunningActivity.Commands;

public class DeleteRunningActivityByIdCommandHandlerTests
{
    [Fact]
    public void Handle_Should_DeleteRunningActivity()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new DeleteRunningActivityByIdCommandHandler(runningActivityRepository.Object, unitOfWork.Object);

        runningActivityRepository.Setup(x => x.Delete(It.IsAny<Activity>()));
        runningActivityRepository.Setup(runningActivityRepository => runningActivityRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Activity(It.IsAny<Guid>(), It.IsAny<Guid>(), "Mandaluyong City", DateTime.Now, DateTime.Now.AddMinutes(280), 13));

        unitOfWork.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var validator = new DeleteRunningActivityByIdCommandValidator();
        var command = new DeleteRunningActivityByIdCommand(Guid.NewGuid());

        // Act
        var validate = validator.Validate(command);
        var result = handler.Handle(command, CancellationToken.None)?.Result;

        // Assert
        validate.IsValid.ShouldBeTrue();
        result.ShouldBeOfType<ServiceResponse>();
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public void Handle_Should_ReturnError_WhenRunningActivityIsNotDeleted()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var handler = new DeleteRunningActivityByIdCommandHandler(runningActivityRepository.Object, unitOfWork.Object);

        runningActivityRepository.Setup(x => x.Delete(It.IsAny<Activity>()));
        runningActivityRepository.Setup(runningActivityRepository => runningActivityRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Activity)null);

        unitOfWork.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var validator = new DeleteRunningActivityByIdCommandValidator();
        var command = new DeleteRunningActivityByIdCommand(It.IsAny<Guid>());

        // Act
        var validate = validator.Validate(command);
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        validate.IsValid.ShouldBeFalse();
        var exception = act.ShouldThrowAsync<RunningActivityNotFoundException>();
        exception.Result.Message.ShouldContain("was not found");
    }
}
