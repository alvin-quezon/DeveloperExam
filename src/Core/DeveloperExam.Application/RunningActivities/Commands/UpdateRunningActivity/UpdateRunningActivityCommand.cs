using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.RunningActivities.Commands.UpdateRunningActivity;

public sealed record UpdateRunningActivityCommand(Guid Id, string Location, DateTime Start, DateTime End, double Distance) : ICommand<ServiceResponse>;
