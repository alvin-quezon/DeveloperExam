namespace DeveloperExam.Application.RunningActivities.Commands.UpdateRunningActivity;

public sealed record UpdateRunningActivityRequest(string Location, DateTime Start, DateTime End, double Distance);
