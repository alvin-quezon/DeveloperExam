using DeveloperExam.Application.Dto;
using DeveloperExam.Application.UserProfiles.Queries.GetAllUserProfiles;
using DeveloperExam.Domain.Abstractions;
using Moq;
using Shouldly;
using User = DeveloperExam.Domain.Entities.UserProfile;

namespace DeveloperExam.Application.Tests.UserProfile.Queries;

public class GetAllUserProfilesQueryHandlerTests
{
    [Fact]
    public void Handle_Should_ReturnListOfUserProfile()
    {
        // Arrange
        var userProfileRepository = new Mock<IUserProfileRepository>();

        var query = new GetAllUserProfilesQuery();
        var handler = new GetAllUserProfilesQueryHandler(userProfileRepository.Object);

        userProfileRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<User>
            {
                new User(It.IsAny<Guid>(), "Jose Rizal", 180, 70, DateTime.Now.AddYears(-20)),
                new User(It.IsAny<Guid>(), "Andres Bonifacio", 167, 55, DateTime.Now.AddYears(-20)),
                new User(It.IsAny<Guid>(), "Emilio Aguinaldo", 167, 55, DateTime.Now.AddYears(-20)),
            });

        // Act
        var result = handler.Handle(query, CancellationToken.None)?.Result.ToList();

        // Assert
        result.ShouldBeOfType<List<UserProfileResponse>>();
        result.Count().ShouldBe(3);
    }
}
