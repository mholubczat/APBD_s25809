using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp
{
    public interface IClientRepository
    {
        internal Client GetById(int clientId);
    }
    
    public class ClientRepository : IClientRepository
    {
        /// <summary>
        /// This collection is used to simulate remote database
        /// </summary>
        private static readonly Dictionary<int, Client> Database = new()
        {
            {1, new Client{ClientId = 1, Name = "Kowalski", Address = "Warszawa, Złota 12", Email = "kowalski@wp.pl", Type = ClientType.NormalClient}},
            {2, new Client{ClientId = 2, Name = "Malewski", Address = "Warszawa, Koszykowa 86", Email = "malewski@gmail.pl", Type = ClientType.VeryImportantClient}},
            {3, new Client{ClientId = 3, Name = "Smith", Address = "Warszawa, Kolorowa 22", Email = "smith@gmail.pl", Type = ClientType.ImportantClient}},
            {4, new Client{ClientId = 4, Name = "Doe", Address = "Warszawa, Koszykowa 32", Email = "doe@gmail.pl", Type = ClientType.ImportantClient}},
            {5, new Client{ClientId = 5, Name = "Kwiatkowski", Address = "Warszawa, Złota 52", Email = "kwiatkowski@wp.pl", Type = ClientType.NormalClient}},
            {6, new Client{ClientId = 6, Name = "Andrzejewicz", Address = "Warszawa, Koszykowa 52", Email = "andrzejewicz@wp.pl", Type = ClientType.NormalClient}}
        };

        /// <summary>
        /// Simulating fetching a client from remote database
        /// </summary>
        /// <returns>Returning client object</returns>
        public Client GetById(int clientId)
        {
            var randomWaitTime = new Random().Next(2000);
            Thread.Sleep(randomWaitTime);

            if (Database.TryGetValue(clientId, out var id))
            {
                return id;
            }

            throw new ArgumentException($"User with id {clientId} does not exist in database");
        }
    }
}