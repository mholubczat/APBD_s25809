using Trip.Models;
using Trip.Repositories;

namespace Trip.Service;

public interface IClientService
{
    public Task<Client> GetClient(int idClient, CancellationToken cancellationToken);
    public Task<bool> DeleteClient(int idClient, CancellationToken cancellationToken);
}

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<Client> GetClient(int idClient, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetClient(idClient, cancellationToken);

        return client;
    }

    public async Task<bool> DeleteClient(int idClient, CancellationToken cancellationToken)
    {
        var client = await GetClient(idClient, cancellationToken);
        
        if (client.ClientTrips.Count != 0)
        {
            return false;
        }
        
        await clientRepository.DeleteClient(client, cancellationToken);
        return true;
    }
}