using Microsoft.EntityFrameworkCore;
using Trip.Context;
using Trip.Models;

namespace Trip.Repositories;

public interface IClientRepository
{
    Task<Client> GetClient(int idClient, CancellationToken cancellationToken);
    Task DeleteClient(Client client, CancellationToken cancellationToken);
}

public class ClientRepository(TripAppContext tripAppContext) : IClientRepository
{
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