using System;
using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.RawMaterial;

namespace Cafeteria.Mapping;

public class RawMaterialProfile:Profile
{
    public RawMaterialProfile()
    {
        CreateMap<RawMaterial, RawMaterialDto>()
        .ForMember(dest => dest.UnitOfMeasurement, opt => opt.MapFrom(src => src.UnitOfMeasurement.Name))
        .ReverseMap();
        CreateMap<RawMaterial, CreateRawMatetialDto>().ReverseMap();
        CreateMap<RawMaterial, UpdateRawMaterialDto>().ReverseMap();
    }
}
