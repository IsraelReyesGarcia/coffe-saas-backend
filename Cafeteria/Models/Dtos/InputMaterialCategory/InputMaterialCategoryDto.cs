using System.ComponentModel.DataAnnotations;
namespace Cafeteria.Models.Dtos.InputMaterialCategory;

public class InputMaterialCategoryDto
{
    public int InputMaterialCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SuggestionUssageType { get; set; }
}