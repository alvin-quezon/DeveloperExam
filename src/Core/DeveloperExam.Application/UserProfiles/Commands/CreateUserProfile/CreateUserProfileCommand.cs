using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.UserProfiles.Commands.CreateUserProfile;

public sealed record CreateUserProfileCommand(string Name, double Weight, double Height, DateOnly BirthDate) : ICommand<ServiceResponse>;