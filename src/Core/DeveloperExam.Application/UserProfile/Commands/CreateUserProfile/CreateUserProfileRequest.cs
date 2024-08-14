namespace DeveloperExam.Application.UserProfile.Commands.CreateUserProfile;

public sealed record CreateUserProfileRequest(string Name, double Weight, double Height, DateTime BirthDate);