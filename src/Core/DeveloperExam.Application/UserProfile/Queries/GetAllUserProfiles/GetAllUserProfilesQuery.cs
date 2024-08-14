using DeveloperExam.Application.Abstractions.Messaging;

namespace DeveloperExam.Application.UserProfile.Queries.GetAllUserProfiles;

public sealed record GetAllUserProfilesQuery : IQuery<IEnumerable<GetAllUserProfilesResponse>>;