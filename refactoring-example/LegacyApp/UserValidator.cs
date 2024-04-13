namespace LegacyApp;

public interface IUserValidator
{
    public bool Validate(User user);
    public bool ValidateCredit(User user);
}

public class UserValidator : IUserValidator
{
    private const int MinAge = 21;
    private const int MinCreditLimit = 500;

    public bool Validate(User user)
    {
        if(string.IsNullOrEmpty(user.FirstName) ||  string.IsNullOrEmpty(user.LastName))
        {
            return false;
        }

        if (user.Age < MinAge)
        {
            return false;
        }

        return user.EmailAddress.Contains('@') && user.EmailAddress.Contains('.');
    }

    public bool ValidateCredit(User user)
    {
        return user.HasCreditLimit == false || user.CreditLimit >= MinCreditLimit;
    }
}