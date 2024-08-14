using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.RunningActivities.Commands.DeleteRunningActivityById;

public sealed record DeleteRunningActivityByIdCommand(Guid Id) : ICommand<ServiceResponse>;