using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.UserProfiles.Queries.GetAllUserProfiles;

public sealed record GetAllUserProfilesQuery : IQuery<IEnumerable<UserProfileResponse>>;