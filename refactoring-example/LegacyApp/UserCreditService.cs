using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp
{
    public interface IUserCreditService
    {
        internal int GetCreditLimit(User user);
    }
    
    public class UserCreditService : IDisposable, IUserCreditService
    {
        private const int ImportantClientCreditMultiplier = 2;
        
        /// <summary>
        /// Simulating database
        /// </summary>
        private readonly Dictionary<string, int> _database =
            new()
            {
                {"Kowalski", 200},
                {"Malewski", 20000},
                {"Smith", 10000},
                {"Doe", 3000},
                {"Kwiatkowski", 1000}
            };
        
        public void Dispose()
        {
            //Simulating disposing of resources
        }

        public int GetCreditLimit(User user)
        {
            return user.Client.Type
                switch
                {
                    ClientType.VeryImportantClient =>
                        throw new ArgumentException(
                            nameof(ClientType.VeryImportantClient) + " has no credit limit"),
                    
                    ClientType.ImportantClient => GetCreditLimit(user.LastName, user.DateOfBirth) *
                                                  ImportantClientCreditMultiplier,
                    
                    ClientType.NormalClient => GetCreditLimit(user.LastName, user.DateOfBirth),
                    
                    _ => throw new ArgumentOutOfRangeException(nameof(user.Client.Type))
                };
        }

        /// <summary>
        /// This method is simulating contact with remote service which is used to get info about someone's credit limit
        /// </summary>
        /// <returns>Client's credit limit</returns>
        private int GetCreditLimit(string lastName, DateTime dateOfBirth)
        {
            var randomWaitingTime = new Random().Next(3000);
            Thread.Sleep(randomWaitingTime);

            if (_database.TryGetValue(lastName, out var value))
            {
                return value;
            }

            throw new ArgumentException($"Client {lastName} does not exist");
        }
    }
}