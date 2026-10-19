using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Warehouse;
namespace Cafeteria.Mapping;

public class WarehouseProfile:Profile
{
    public WarehouseProfile()
    {
        CreateMap<Warehouse, WarehouseDto>().ReverseMap();
        CreateMap<Warehouse, CreateWarehouseDto>().ReverseMap();
        CreateMap<Warehouse, UpdateWarehouseDto>().ReverseMap();
    }
}
