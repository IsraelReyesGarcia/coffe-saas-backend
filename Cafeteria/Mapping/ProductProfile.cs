using System;
using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Product;

namespace Cafeteria.Mapping;

public class ProductProfile:Profile
{
    public ProductProfile()
    {
        CreateMap<Product,ProductDto>().ReverseMap();
        CreateMap<Product,CreateProductDto>().ReverseMap();
        CreateMap<Product,ProductResponseDto>()
        .ForMember(dest => dest.ProductCategorie, opt => opt.MapFrom(src => src.ProductCategorie != null ? src.ProductCategorie.Category : string.Empty));
    }
}
