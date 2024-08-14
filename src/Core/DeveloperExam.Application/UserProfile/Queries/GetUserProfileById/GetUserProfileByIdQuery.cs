using DeveloperExam.Application.Abstractions.Messaging;

namespace DeveloperExam.Application.UserProfile.Queries.GetUserProfileById;

public sealed record GetUserProfileByIdQuery(Guid Id) : IQuery<GetUserProfileByIdResponse>;