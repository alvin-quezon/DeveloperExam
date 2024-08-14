using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.RunningActivities.Commands.CreateRunningActivity;

public sealed record CreateRunningActivityCommand(Guid UserProfileId, string Location, DateTime Start, DateTime End, double Distance) : ICommand<ServiceResponse>;