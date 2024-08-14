using DeveloperExam.Application.Abstractions.Messaging;

namespace DeveloperExam.Application.UserProfile.Queries.GetAllUserProfiles;

public sealed class GetAllUserProfilesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }
    public double BodyMassIndex { get; set; }
}