using System;
using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.UnitOfMeasurement;

namespace Cafeteria.Mapping;

public class UnitOfMeasurementProfile:Profile
{
    public UnitOfMeasurementProfile()
    {
        CreateMap<UnitOfMeasurement, UnitOfMeasurementDto>().ReverseMap();
        CreateMap<UnitOfMeasurement, CreateUnitOfMeasurementDto>().ReverseMap();
        CreateMap<UnitOfMeasurement, UpdateUnitOfMeasurementDto>().ReverseMap();
    }
}
