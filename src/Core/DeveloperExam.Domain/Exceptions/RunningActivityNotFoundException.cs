using DeveloperExam.Domain.Exceptions.Base;

namespace DeveloperExam.Domain.Exceptions;

public class RunningActivityNotFoundException : NotFoundException
{
    public RunningActivityNotFoundException(Guid activityId) : base($"The activity with the identifier {activityId} was not found.")
    {
    }
}