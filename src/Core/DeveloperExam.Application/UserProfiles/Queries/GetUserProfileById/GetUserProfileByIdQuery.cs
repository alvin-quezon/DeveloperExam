using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.UserProfiles.Queries.GetUserProfileById;

public sealed record GetUserProfileByIdQuery(Guid Id) : IQuery<UserProfileResponse>;