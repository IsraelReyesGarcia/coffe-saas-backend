using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.ProductCategorie;

namespace Cafeteria.Mapping;

public class ProductCategorieProfile:Profile
{
    public ProductCategorieProfile()
    {
        CreateMap<ProductCategorie,ProductCategorieDto>().ReverseMap();
        CreateMap<ProductCategorie, CreateProductCategorieDto>().ReverseMap();
    }
}
