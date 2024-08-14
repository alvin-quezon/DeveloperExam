using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.UserProfile.Commands.UpdateUserProfile;

public sealed record UpdateUserProfileCommand(Guid Id, string Name, double Weight, double Height, DateTime BirthDate) : ICommand<ServiceResponse>;