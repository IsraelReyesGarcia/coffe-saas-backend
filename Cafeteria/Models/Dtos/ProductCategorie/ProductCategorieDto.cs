using System.ComponentModel.DataAnnotations.Schema;
namespace Cafeteria.Models.Dtos.ProductCategorie;

public class ProductCategorieDto
{
    public int ProductCategorieId {get;set;}

    public string Category {get; set;} = String.Empty;
}
