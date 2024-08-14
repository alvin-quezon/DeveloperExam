using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.RunningActivities.Queries.GetRunningActivityById;

public sealed record GetRunningActivityByIdQuery(Guid Id) : IQuery<RunningActivityResponse>;