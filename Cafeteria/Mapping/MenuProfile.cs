using System;
using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Menu;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cafeteria.Mapping;

public class MenuProfile:Profile
{
    public MenuProfile()
    {
        CreateMap<Menu,MenuDto>()
        .ForMember(dest => dest.MenuType, opt => opt.MapFrom(src => src.MenuTime.Name))
        .ReverseMap();
        CreateMap<Menu,CreateMenuDto>().ReverseMap();
        CreateMap<Menu,UpdateMenuDto>().ReverseMap();
    }
}
