using System.ComponentModel.DataAnnotations.Schema;
namespace Cafeteria.Models.Dtos.ProductCategorie;

public class CreateProductCategorieDto
{
    public string Category {get; set;} = String.Empty;
}
