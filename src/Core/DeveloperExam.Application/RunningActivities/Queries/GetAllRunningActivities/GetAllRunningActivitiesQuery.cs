using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.RunningActivities.Queries.GetAllRunningActivities;

public sealed record GetAllRunningActivitiesQuery : IQuery<IEnumerable<RunningActivityResponse>>;