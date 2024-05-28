using Microsoft.EntityFrameworkCore;
using Trip.Context;
using Trip.Models;

namespace Trip.Repositories;

public interface IClientRepository
{
    Task<Client> GetOrAddClient(Client client, CancellationToken cancellationToken);
    Task<Client> GetClient(int idClient, CancellationToken cancellationToken);
    Task DeleteClient(Client client, CancellationToken cancellationToken);
}

public class ClientRepository(TripAppContext tripAppContext) : IClientRepository
{
    public async Task<Client> GetOrAddClient(Client client, CancellationToken cancellationToken)
    {
        var existingClient = await tripAppContext
            .Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(existingClient => existingClient.Pesel == client.Pesel, cancellationToken);
        if (existingClient != null)
        {
            return existingClient;
        }

        tripAppContext.Clients.Add(client);
        await tripAppContext.SaveChangesAsync(cancellationToken);
        return client;
    }

    public async Task<Client> GetClient(int idClient, CancellationToken cancellationToken)
    {
        var client = await tripAppContext
            .Clients
            .Include(c => c.ClientTrips)
            .SingleAsync(client => client.IdClient == idClient, cancellationToken);

        return client;
    }

    public async Task DeleteClient(Client client, CancellationToken cancellationToken)
    {
        tripAppContext.Clients.Remove(client);

        await tripAppContext.SaveChangesAsync(cancellationToken);
    }
}