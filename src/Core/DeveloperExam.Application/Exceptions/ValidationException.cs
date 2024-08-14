using DeveloperExam.Domain.Exceptions.Base;

namespace DeveloperExam.Application.Exceptions;

public class ValidationException : BadRequestException
{
    public ValidationException(Dictionary<string, string[]> errors) : base($"Validation failed with total errors of {errors.Count}")
        => Errors = errors;

    public Dictionary<string, string[]> Errors { get; set; }
}