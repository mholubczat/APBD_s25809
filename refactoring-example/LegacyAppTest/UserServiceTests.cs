﻿using LegacyApp;
using Moq;

namespace LegacyAppTest;

public class UserServiceTests
{
    private UserService _userService;
    private Mock<IClientRepository> _clientRepositoryMock;
    private Mock<IUserCreditService> _userCreditServiceMock;

    private void SetupMocksToReturnClientWithCredit(Client client, int credit)
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _clientRepositoryMock
            .Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(client);

        _userCreditServiceMock = new Mock<IUserCreditService>();
        _userCreditServiceMock
            .Setup(x => x.GetCreditLimit(It.IsAny<User>()))
            .Returns(credit);
        
        _userService = new UserService(
            _clientRepositoryMock.Object,
            _userCreditServiceMock.Object,
            new UserValidator());
    }
    
    private readonly Client _johnRambo = new()
    {
        Name = "John Rambo",
        ClientId = 1,
        Email = "john.rambo@gmail.com",
        Address = "Jungle",
        Type = ClientType.NormalClient
    };

    private readonly DateTime _dateOfBirth = new(1980,1,1);

    [Test]
    [TestCase("", "Rambo", false)]
    [TestCase(null, "Rambo", false)]
    [TestCase("John", "", false)]
    [TestCase("John", null, false)]
    [TestCase("John", "Rambo", true)]
    public void AddUser_EmptyName_ShouldReturnFalse(string? firstName, string? lastName, bool result)
    {
        //Arrange
        SetupMocksToReturnClientWithCredit(_johnRambo, 1000);
        
        // Act
        var isUserAdded = _userService.AddUser(firstName, lastName, _johnRambo.Email, _dateOfBirth, _johnRambo.ClientId);
        
        // Assert
        Assert.That(result, Is.EqualTo(isUserAdded));
    }
    
    [Test]
    [TestCase(1, false)]
    [TestCase(-1, true)]
    public void AddUser_Underage_ShouldReturnFalse(int monthsTo21Birthday, bool result)
    {
        //Arrange
        SetupMocksToReturnClientWithCredit(_johnRambo, 1000);
        var dateOfBirth = DateTime.Now.AddYears(-21).AddMonths(monthsTo21Birthday);
        
        // Act
        var isUserAdded = _userService.AddUser("John", "Rambo", _johnRambo.Email, dateOfBirth, _johnRambo.ClientId);
        
        // Assert
        Assert.That(result, Is.EqualTo(isUserAdded));
    }
    
    [Test]
    [TestCase("john.rambo.gmail.com", false)]
    [TestCase("johnrambo@gmailcom", false)]
    [TestCase("john.rambo@gmail.com", true)]
    public void AddUser_InvalidEmail_ShouldReturnFalse(string email, bool result)
    {
        //Arrange
        SetupMocksToReturnClientWithCredit(_johnRambo, 1000);
        
        // Act
        var isUserAdded = _userService.AddUser("John", "Rambo", email, _dateOfBirth, _johnRambo.ClientId);
        
        // Assert
        Assert.That(result, Is.EqualTo(isUserAdded));
    }
    
    [Test]
    [TestCase(499, false)]
    [TestCase(500, true)]
    public void AddUser_InsufficientCredit_ShouldReturnFalse(int credit, bool result)
    {
        //Arrange
        SetupMocksToReturnClientWithCredit(_johnRambo, credit);
        
        // Act
        var isUserAdded = _userService.AddUser("John", "Rambo", _johnRambo.Email, _dateOfBirth, _johnRambo.ClientId);
        
        // Assert
        Assert.That(result, Is.EqualTo(isUserAdded));
    }
    
    [Test]
    [TestCase(ClientType.NormalClient, false)]
    [TestCase(ClientType.ImportantClient, true)]
    [TestCase(ClientType.VeryImportantClient, true)]
    public void AddUser_ImportantClients_ShouldReturnTrue(ClientType type, bool result)
    {
        //Arrange
        var client = new Client
        {
            Name = _johnRambo.Name,
            ClientId = _johnRambo.ClientId,
            Email = _johnRambo.Email,
            Address = _johnRambo.Address,
            Type = type
        };
        
        SetupMocksToReturnClientWithCredit(client, 0);
        
        // Act
        var isUserAdded = _userService.AddUser("John", "Rambo", _johnRambo.Email, _dateOfBirth, _johnRambo.ClientId);
        
        // Assert
        Assert.That(result, Is.EqualTo(isUserAdded));
    }
}