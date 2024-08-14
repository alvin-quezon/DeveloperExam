using Activity = DeveloperExam.Domain.Entities.RunningActivity;
using Shouldly;

namespace DeveloperExam.Domain.Tests.RunningActivity;

public class RunningActivityTests
{
    [Fact]
    public void RunningActivity_Should_CalculateCorrectly_WhenDataIsCorrect()
    {
        // Arrange
        var calculateRunningActivity = new Activity(Guid.NewGuid(), Guid.NewGuid(), "Mandaluyong City", DateTime.Now, DateTime.Now.AddMinutes(280), 11);
        
        // Act
        var duration = calculateRunningActivity.End.TimeOfDay - calculateRunningActivity.Start.TimeOfDay;
        var averagePace = duration.TotalHours == 0 || calculateRunningActivity.Distance == 0 ? 0 : duration.TotalHours / calculateRunningActivity.Distance;

        // Assert
        duration.ShouldBe(calculateRunningActivity.Duration);
        averagePace.ShouldBe(calculateRunningActivity.AveragePace);
    }
}
