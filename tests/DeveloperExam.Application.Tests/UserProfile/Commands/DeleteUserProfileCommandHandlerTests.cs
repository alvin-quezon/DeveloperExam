using DeveloperExam.Application.UserProfiles.Commands.DeleteUserProfile;
using Moq;
using DeveloperExam.Domain.Abstractions;
using User = DeveloperExam.Domain.Entities.UserProfile;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.Tests.UserProfile.Commands
{
    public class DeleteUserProfileCommandHandlerTests
    {
        [Fact]
        public void Handle_Should_ReturnSuccessResult_WhenUserProfileIsDeleted()
        {
            // Arrange
            var mockRepository = new Mock<IUserProfileRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new DeleteUserProfileCommandHandler(mockRepository.Object, mockUnitOfWork.Object);

            mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User("Test", 1, 1, DateTime.Now));
            mockUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var validator = new DeleteUserProfileCommandValidator();
            var command = new DeleteUserProfileCommand(Guid.NewGuid());

            // Act
            var validate = validator.Validate(command);
            var result = handler.Handle(command, new CancellationToken()).Result;

            // Assert
            Assert.True(validate.IsValid);
            Assert.True(result.Success);
            Assert.Equal("User profile deleted successfully", result.Message);
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_WhenUserProfileIsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IUserProfileRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new DeleteUserProfileCommandHandler(mockRepository.Object, mockUnitOfWork.Object);

            mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);

            var validator = new DeleteUserProfileCommandValidator();
            var command = new DeleteUserProfileCommand(Guid.NewGuid());

            // Act
            var validate = validator.Validate(command);
            Func<Task> act = async () => await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(validate.IsValid);
            var exception = Assert.ThrowsAsync<UserProfileNotFoundException>(act);
            Assert.Contains("not found", exception.Result.Message);
        }
    }
}
