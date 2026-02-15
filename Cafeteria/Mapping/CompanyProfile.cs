using System;
using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Company;

namespace Cafeteria.Mapping;

public class CompanyProfile:Profile
{
    public CompanyProfile()
    {
        CreateMap<Company,CompanyDto>().ReverseMap();
        CreateMap<Company,CreateCompanyDto>().ReverseMap();
    }
}
