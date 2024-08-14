using DeveloperExam.Application.RunningActivities.Queries.GetAllRunningActivities;
using DeveloperExam.Domain.Abstractions;
using Activity = DeveloperExam.Domain.Entities.RunningActivity;
using Moq;
using Shouldly;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.Tests.RunningActivity.Queries;

public class GetAllRunningActivitiesQueryHandlerTests
{
    [Fact]
    public void Handle_Should_ReturnListOfRunningActivity()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();

        var query = new GetAllRunningActivitiesQuery();
        var handler = new GetAllRunningActivitiesQueryHandler(runningActivityRepository.Object);

        runningActivityRepository.Setup(x => x.GetAllRunningActivitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Activity>
            {
                new Activity(It.IsAny<Guid>(), It.IsAny<Guid>(), "Mandaluyong City", DateTime.Now, DateTime.Now.AddMinutes(280), 13),
                new Activity(It.IsAny<Guid>(), It.IsAny<Guid>(), "Manila City", DateTime.Now, DateTime.Now.AddMinutes(360), 20),
                new Activity(It.IsAny<Guid>(), It.IsAny<Guid>(), "Makati City", DateTime.Now, DateTime.Now.AddMinutes(220), 10),
            });

        // Act
        var result = handler.Handle(query, CancellationToken.None)?.Result.ToList();

        // Assert
        result.ShouldBeOfType<List<RunningActivityResponse>>();
        result.Count().ShouldBe(3);
    }
}
