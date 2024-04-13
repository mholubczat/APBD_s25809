using System;

namespace LegacyApp
{
    public class User
    {
        public Client Client { get; internal init; }
        public DateTime DateOfBirth { get; internal init; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal init; }
        public bool HasCreditLimit => Client.Type == ClientType.NormalClient;
        public int CreditLimit { get; internal set; }
    }
}