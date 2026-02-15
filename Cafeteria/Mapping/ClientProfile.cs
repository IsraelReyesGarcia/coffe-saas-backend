using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Client;

namespace Cafeteria.Mapping;

public class ClientProfile:Profile
{
    public ClientProfile()
    {
        CreateMap<Client,ClientDto>().ReverseMap();
        CreateMap<Client,CreateClientDto>().ReverseMap();
        CreateMap<Client,UpdateClientDto>().ReverseMap();
    }
}
