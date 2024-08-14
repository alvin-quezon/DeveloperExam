namespace DeveloperExam.Domain.Exceptions.Base;

public class NotFoundException : Exception
{
    protected NotFoundException(string message) : base(message)
    {
    }
}