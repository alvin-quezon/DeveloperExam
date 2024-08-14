namespace DeveloperExam.Application.RunningActivities.Commands.CreateRunningActivity;

public sealed record CreateRunningActivityRequest(Guid UserProfileId, string Location, DateTime Start, DateTime End, double Distance);