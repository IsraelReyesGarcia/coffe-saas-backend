using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.InputMaterialCategory;

namespace Cafeteria.Mapping;

public class InputMaterialCategoryProfile:Profile
{
    public InputMaterialCategoryProfile()
    {
        CreateMap<InputMaterialCategory, InputMaterialCategoryDto>().ReverseMap();
    }
}
