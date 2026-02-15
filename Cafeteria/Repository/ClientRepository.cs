using Cafeteria.Models.Dtos.Client;
using Cafeteria.Repository.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cafeteria.Repository;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _db;
    
    public ClientRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool ClientExist(int id)
    {
        if(id <= 0)
        {
            return false;
        }

        return _db.Clients.Any(c => c.ClientId == id);
    }

    public async Task<ClientDto> CreateClient(CreateClientDto createClientDto)
    {
        if(createClientDto == null)
        {
            return null;
        }

        var client = new Cafeteria.Models.Client
        {
            Name = createClientDto.Name,
            Email = createClientDto.Email,
            Phone = createClientDto.Phone,
            Rfc = createClientDto.Rfc,
            Rating = createClientDto.Rating,
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow
        };

        _db.Clients.Add(client);
        await _db.SaveChangesAsync();

        return new ClientDto
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone,
            Rfc = client.Rfc,
            Rating = client.Rating
        };

    }

    public async Task<ClientDto> UpdateClient(UpdateClientDto updateClientDto)
    {
        if(updateClientDto == null)
        {
            return null;
        }
        
        var client = new Cafeteria.Models.Client
        {
            ClientId = updateClientDto.ClientId,
            Name = updateClientDto.Name,
            Email = updateClientDto.Email,
            Phone = updateClientDto.Phone,
            Rfc = updateClientDto.Rfc,
            Rating = updateClientDto.Rating
        };

        _db.Clients.Update(client);
        await _db.SaveChangesAsync();

        return new ClientDto
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone,
            Rfc = client.Rfc,
            Rating = client.Rating
        };
    }

    public bool DeleteClient(ClientDto clientDto)
    {
        if(clientDto == null)
        {
            return false;
        }

        var client = _db.Clients.FirstOrDefault(c => c.ClientId == clientDto.ClientId);
        _db.Clients.Remove(client);

        return Save();

    }

    public ClientDto? GetClient(int id)
    {
        if(id <= 0)
        {
            return null;
        }

        var client = _db.Clients.FirstOrDefault(c => c.ClientId == id);
        if (client == null) return null;

        return new ClientDto
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone,
            Rfc = client.Rfc,
            Rating = client.Rating
        };
    }

    public ICollection<ClientDto> GetClients()
    {
        return _db.Clients
            .OrderBy(c => c.Name)
            .Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Rfc = c.Rfc,
                Rating = c.Rating
            })
            .ToList();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }
}
