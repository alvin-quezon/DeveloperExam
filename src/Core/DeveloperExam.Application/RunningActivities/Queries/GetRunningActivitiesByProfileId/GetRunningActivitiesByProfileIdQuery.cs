using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.RunningActivities.Queries.GetRunningActivitiesByProfileId;

public sealed record GetRunningActivitiesByProfileIdQuery(Guid Id) : IQuery<IEnumerable<RunningActivityResponse>>;
