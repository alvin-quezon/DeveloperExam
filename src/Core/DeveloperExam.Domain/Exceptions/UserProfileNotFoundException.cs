using DeveloperExam.Domain.Exceptions.Base;

namespace DeveloperExam.Domain.Exceptions;

public class UserProfileNotFoundException : NotFoundException
{
    public UserProfileNotFoundException(Guid profileId) : base($"The profile with the identifier {profileId} was not found.")
    {
    }
}