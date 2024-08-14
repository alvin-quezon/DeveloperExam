namespace DeveloperExam.Application.UserProfile.Queries.GetUserProfileById;

public sealed record GetUserProfileByIdResponse(Guid Id, string Name, double Weight, double Height, DateTime BirthDate, int Age, double BodyMassIndex);
