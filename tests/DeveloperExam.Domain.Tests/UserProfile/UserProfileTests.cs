using Shouldly;
using User = DeveloperExam.Domain.Entities.UserProfile;

namespace DeveloperExam.Domain.Tests.UserProfile;

public class UserProfileTests
{
    [Fact]
    public void Handle_Should_CorrectlyCalculate_WhenDataIsCorrect()
    {
        // Arrange
        var calculateUserProfile = new User("John Someone", 66, 178, new DateTime(1992, 08, 03));

        // Act
        var age = DateTime.Now.DayOfYear < calculateUserProfile.BirthDate.DayOfYear ? calculateUserProfile.BirthDate.AddYears(-1).Year : DateTime.Now.Year - calculateUserProfile.BirthDate.Year;
        var bmi = Math.Round(calculateUserProfile.Weight / Math.Pow(calculateUserProfile.Height / 100, 2), 2);

        // Assert
        calculateUserProfile.Age.ShouldBe(age);
        Math.Round(calculateUserProfile.BodyMassIndex, 2).ShouldBe(bmi);
    }
}
