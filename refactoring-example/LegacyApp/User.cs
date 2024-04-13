using System;

namespace LegacyApp;

public class User
{
    public Client Client { get; internal set; }
    public DateTime DateOfBirth { get; internal init; }
    public string EmailAddress { get; internal init; }
    public string FirstName { get; internal init; }
    public string LastName { get; internal init; }
    public bool HasCreditLimit => Client.Type == ClientType.NormalClient;
    public int CreditLimit { get; internal set; }
    public int Age => GetAge(DateOfBirth);

    private static int GetAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;

        return dateOfBirth < now.AddYears(-age) ? age : age - 1;
    }
}