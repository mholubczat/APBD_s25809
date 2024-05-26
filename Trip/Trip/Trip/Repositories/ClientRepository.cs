using Microsoft.EntityFrameworkCore;
using Trip.Context;
using Trip.Models;

namespace Trip.Repositories;

public interface IClientRepository
{
    Task TryAddClient(Client client, CancellationToken cancellationToken);
    Task<Client> GetClient(int idClient, CancellationToken cancellationToken);
    Task DeleteClient(Client client, CancellationToken cancellationToken);
}

public class ClientRepository(TripAppContext tripAppContext) : IClientRepository
{
    public async Task TryAddClient(Client client, CancellationToken cancellationToken)
    {
        var isAlreadyAdded = await tripAppContext.Clients.AnyAsync(existingClient => existingClient.Pesel == client.Pesel, cancellationToken);
        if (isAlreadyAdded)
        {
            return;
        }

        tripAppContext.Clients.Add(client);
        await tripAppContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Client> GetClient(int idClient, CancellationToken cancellationToken)
    {
        var client = await tripAppContext.Clients.SingleAsync(client => client.IdClient == idClient, cancellationToken);

        return client;
    }

    public async Task DeleteClient(Client client, CancellationToken cancellationToken)
    {
        tripAppContext.Clients.Remove(client);

        await tripAppContext.SaveChangesAsync(cancellationToken);
    }
}