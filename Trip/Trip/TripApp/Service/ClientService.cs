using Trip.Models;
using Trip.Repositories;

namespace Trip.Service;

public interface IClientService
{
    public Task AddClient(Client client, CancellationToken cancellationToken);
    public Task<Client> GetClient(int idClient, CancellationToken cancellationToken);
    public Task DeleteClient(int idClient, CancellationToken cancellationToken);
}

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task AddClient(Client client, CancellationToken cancellationToken)
    {
        await clientRepository.AddClient(client, cancellationToken);
    }

    public async Task<Client> GetClient(int idClient, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetClient(idClient, cancellationToken);

        return client;
    }

    public async Task DeleteClient(int idClient, CancellationToken cancellationToken)
    {
        var client = await GetClient(idClient, cancellationToken);

        if (client.ClientTrips.Count != 0)
        {
            throw new Exception("Cannot delete a client assigned to a trip");
        }

        await clientRepository.DeleteClient(client, cancellationToken);
    }
}