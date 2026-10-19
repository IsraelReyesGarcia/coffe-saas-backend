using System;
using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos;

namespace Cafeteria.Mapping;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<ApplicationRole,RoleDto>().ReverseMap();
        CreateMap<ApplicationRole,CreateRoleDto>().ReverseMap();
    }
}
