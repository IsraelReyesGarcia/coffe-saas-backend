using System;
using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos;

namespace Cafeteria.Mapping;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<Role,RoleDto>().ReverseMap();
        CreateMap<Role,CreateRoleDto>().ReverseMap();
    }
}
