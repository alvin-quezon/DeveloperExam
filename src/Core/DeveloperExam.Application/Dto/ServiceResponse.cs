namespace DeveloperExam.Application.Dto;

public sealed record ServiceResponse(bool Success, string Message = default);