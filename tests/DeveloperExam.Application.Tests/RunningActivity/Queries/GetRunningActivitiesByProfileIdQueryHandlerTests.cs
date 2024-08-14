using DeveloperExam.Application.RunningActivities.Queries.GetRunningActivitiesByProfileId;
using DeveloperExam.Domain.Abstractions;
using Activity = DeveloperExam.Domain.Entities.RunningActivity;
using Moq;
using Shouldly;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.Tests.RunningActivity.Queries;

public class GetRunningActivitiesByProfileIdQueryHandlerTests
{
    [Fact]
    public void Handle_Should_ReturnListOfRunningActivity()
    {
        // Arrange
        var runningActivityRepository = new Mock<IRunningActivityRepository>();

        var query = new GetRunningActivitiesByProfileIdQuery(It.IsAny<Guid>());
        var handler = new GetRunningActivitiesByProfileIdQueryHandler(runningActivityRepository.Object);

        runningActivityRepository.Setup(x => x.GetRunningActivitiesByUserProfileIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Activity>
            {
                new Activity(It.IsAny<Guid>(), It.IsAny<Guid>(), "Mandaluyong City", DateTime.Now, DateTime.Now.AddMinutes(280), 13),
                new Activity(It.IsAny<Guid>(), It.IsAny<Guid>(), "Manila City", DateTime.Now, DateTime.Now.AddMinutes(360), 20)
            });

        // Act
        var result = handler.Handle(query, CancellationToken.None)?.Result.ToList();

        // Assert
        result.ShouldBeOfType<List<RunningActivityResponse>>();
        result.Count().ShouldBe(2);
    }
}
