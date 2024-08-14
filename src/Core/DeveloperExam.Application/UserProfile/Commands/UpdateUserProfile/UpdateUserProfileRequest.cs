namespace DeveloperExam.Application.UserProfile.Commands.UpdateUserProfile;

public sealed record UpdateUserProfileRequest(string Name, double Weight, double Height, DateTime BirthDate);