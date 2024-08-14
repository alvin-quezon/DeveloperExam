using DeveloperExam.Application.UserProfiles.Commands.UpdateUserProfile;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Application.Dto;
using Moq;
using Shouldly;

namespace DeveloperExam.Application.Tests.UserProfile.Commands;

public class UpdateUserProfileCommandHandlerTests
{
    [Fact]
    public async Task Handle_GivenValidRequest_ShouldUpdateUserProfile()
    {
        // Arrange
        var userProfileRepository = new Mock<IUserProfileRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        userProfileRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Domain.Entities.UserProfile("Johnny Bravo", 10, 10, new DateTime(1995, 05, 27)));
        unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new UpdateUserProfileCommandHandler(userProfileRepository.Object, unitOfWork.Object);
        var request = new UpdateUserProfileCommand(Guid.NewGuid(), "Johnny Bravo", 100, 100, new DateOnly(1995, 05, 27));

        var validator = new UpdateUserProfileCommandValidator();

        // Act
        var validate = validator.Validate(request);
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<ServiceResponse>();
        result.Success.ShouldBeTrue();
        validate.IsValid.ShouldBeTrue();
        result.Message.ShouldBe("User profile updated successfully");
    }

    [Fact]
    public async Task Handle_GivenInvalidRequest_ShouldNotUpdateUserProfile()
    {
        // Arrange
        var userProfileRepository = new Mock<IUserProfileRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        userProfileRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Domain.Entities.UserProfile("Johnny Bravo", 10, 10, new DateTime(1995, 05, 27)));
        unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        var handler = new UpdateUserProfileCommandHandler(userProfileRepository.Object, unitOfWork.Object);
        var request = new UpdateUserProfileCommand(Guid.NewGuid(), "", 0, 0, new DateOnly(2050, 08, 03));

        var validator = new UpdateUserProfileCommandValidator();

        // Act
        var validate = validator.Validate(request);
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<ServiceResponse>();
        result.Success.ShouldBeFalse();
        validate.IsValid.ShouldBeFalse();
        result.Message.ShouldBe("User profile failed to update");
    }
}
