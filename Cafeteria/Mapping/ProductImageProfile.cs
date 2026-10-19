using AutoMapper;
using Cafeteria.Models.Dtos.ProductImage;
using Cafeteria.Models;

namespace Cafeteria.Mapping;

public class ProductImageProfile:Profile
{
    public ProductImageProfile()
    {
        CreateMap<ProductImage, ProductImageDto>().ReverseMap();
    }
}
