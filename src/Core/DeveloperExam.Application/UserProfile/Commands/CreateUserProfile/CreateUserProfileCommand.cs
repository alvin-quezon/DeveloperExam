using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.UserProfile.Commands.CreateUserProfile;

public sealed record CreateUserProfileCommand(string Name, double Weight, double Height, DateTime BirthDate) : ICommand<ServiceResponse>;