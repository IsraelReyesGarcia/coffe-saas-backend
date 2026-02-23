using System;
using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.MenuTime;

namespace Cafeteria.Mapping;

public class MenuTimeProfile:Profile
{
    public MenuTimeProfile()
    {
        CreateMap<MenuTime,MenuTimeDto>().ReverseMap();
        CreateMap<MenuTime,CreateMenuTimeDto>().ReverseMap();
        CreateMap<MenuTime,UpdateMenuTimeDto>().ReverseMap();
    }
}
