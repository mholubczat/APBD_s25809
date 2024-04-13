using System;

namespace LegacyApp;

public class UserService(
    IClientRepository clientRepository,
    IUserCreditService userCreditService,
    IUserValidator userValidator)
{
    public UserService() : this(new ClientRepository(), new UserCreditService(), new UserValidator())
    {
    }

    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        var user = new User
        {
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

        if (userValidator.Validate(user) == false)
        {
            return false;
        }

        user.Client = clientRepository.GetById(clientId);

        if (user.Client.Type != ClientType.VeryImportantClient)
        {
            user.CreditLimit = userCreditService.GetCreditLimit(user);
        }

        if (userValidator.ValidateCredit(user) == false)
        {
            return false;
        }

        UserDataAccess.AddUser(user);
        return true;
    }
}