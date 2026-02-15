using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Table;

namespace Cafeteria.Mapping;

public class TableProfileDto:Profile
{
    public TableProfileDto()
    {
        CreateMap<Table,TableDto>().ReverseMap();
        CreateMap<Table,CreateTableDto>().ReverseMap();
    }
}
