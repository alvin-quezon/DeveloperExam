namespace DeveloperExam.Application.UserProfiles.Commands.CreateUserProfile;

public sealed record CreateUserProfileRequest(string Name, double Weight, double Height, DateOnly BirthDate);