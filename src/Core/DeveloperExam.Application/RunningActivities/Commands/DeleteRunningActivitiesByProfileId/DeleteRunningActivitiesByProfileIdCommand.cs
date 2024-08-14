using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivitiesByProfileId;

public sealed record DeleteRunningActivitiesByProfileIdCommand(Guid UserProfileId) : ICommand<ServiceResponse>;