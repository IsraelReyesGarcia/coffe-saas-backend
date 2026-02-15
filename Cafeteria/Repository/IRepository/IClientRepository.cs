using Cafeteria.Models.Dtos.Client;
namespace Cafeteria.Repository.IRepository;

public interface IClientRepository
{
    // Obtener Clients
    ICollection<ClientDto> GetClients();

    //Obtener Client por id
    ClientDto? GetClient(int id);

    //Saber si existe un Client por id
    bool ClientExist(int id);

    //Crear Client
    Task<ClientDto> CreateClient(CreateClientDto createClientDto);

    //Actualizar Client
    Task<ClientDto> UpdateClient(UpdateClientDto updateClientDto);

    //Eliminar Client
    bool DeleteClient(ClientDto clientDto);

    // Guardar cambios
    bool Save();
}
