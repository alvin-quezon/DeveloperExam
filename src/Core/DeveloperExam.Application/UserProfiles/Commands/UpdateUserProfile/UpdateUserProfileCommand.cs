using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using System.ComponentModel.DataAnnotations;

namespace DeveloperExam.Application.UserProfiles.Commands.UpdateUserProfile;

public sealed record UpdateUserProfileCommand(Guid Id, string Name, double Weight, double Height, DateOnly BirthDate) : ICommand<ServiceResponse>;