using Moq;
using Shouldly;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Application.UserProfiles.Queries.GetUserProfileById;
using User = DeveloperExam.Domain.Entities.UserProfile;
using DeveloperExam.Application.Dto;

namespace DeveloperExam.Application.Tests.UserProfile.Queries;

public class GetUserProfileByIdQueryHandlerTests
{
    public void Handle_Should_ReturnUserProfile()
    {
        // Arrange
        var userProfileRepository = new Mock<IUserProfileRepository>();

        var query = new GetUserProfileByIdQuery(It.IsAny<Guid>());
        var handler = new GetUserProfileByIdQueryHandler(userProfileRepository.Object);

        userProfileRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new User(It.IsAny<Guid>(), "Jose Rizal", 180, 70, DateTime.Now.AddYears(-20)));

        // Act
        var result = handler.Handle(query, CancellationToken.None)?.Result;

        // Assert
        result.ShouldBeOfType<UserProfileResponse>();
        result.Name.ShouldBe("Jose Rizal");
    }
}
