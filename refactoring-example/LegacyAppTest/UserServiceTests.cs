using LegacyApp;

namespace LegacyAppTest;

public class UserServiceTests
{
    private readonly UserService _userService = new();

    [Test]
    [TestCase("", "Rambo")]
    [TestCase(null, "Rambo")]
    [TestCase("John", "")]
    [TestCase("John", null)]
    public void AddUser_EmptyName_ShouldReturnFalse(string? firstName, string? lastName)
    {
        // Arrange
        var dateOfBirth = new DateTime(1980, 1, 1);
        const string email = "john.rambo@gmail.com";
        const int clientId = 1;
        
        // Act
        var isUserAdded = _userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        // Assert
        Assert.That(isUserAdded, Is.False);
    }
}