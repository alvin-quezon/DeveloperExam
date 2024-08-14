namespace DeveloperExam.Application.UserProfiles.Commands.UpdateUserProfile;

public sealed record UpdateUserProfileRequest(string Name, double Weight, double Height, DateOnly BirthDate);