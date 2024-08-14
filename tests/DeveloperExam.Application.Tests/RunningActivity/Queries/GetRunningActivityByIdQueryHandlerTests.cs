using DeveloperExam.Application.RunningActivities.Queries.GetRunningActivityById;
using DeveloperExam.Domain.Abstractions;
using Activity = DeveloperExam.Domain.Entities.RunningActivity;
using Moq;
using Shouldly;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.Tests.RunningActivity.Queries;

public class GetRunningActivityByIdQueryHandlerTests
{
    [Fact]
    public void Handle_Should_ReturnRunningActivity()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();

        var query = new GetRunningActivityByIdQuery(It.IsAny<Guid>());
        var handler = new GetRunningActivityByIdQueryHandler(runningActivityRepository.Object);

        runningActivityRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Activity(It.IsAny<Guid>(), It.IsAny<Guid>(), "Mandaluyong City", DateTime.Now, DateTime.Now.AddMinutes(280), 13));

        // Act
        var result = handler.Handle(query, CancellationToken.None)?.Result;

        // Assert
        result.ShouldBeOfType<RunningActivityResponse>();
        result.Location.ShouldBe("Mandaluyong City");
    }
}
