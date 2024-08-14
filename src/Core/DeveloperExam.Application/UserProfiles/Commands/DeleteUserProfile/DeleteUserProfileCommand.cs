using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.UserProfiles.Commands.DeleteUserProfile;

public sealed record DeleteUserProfileCommand(Guid Id) : ICommand<ServiceResponse>;