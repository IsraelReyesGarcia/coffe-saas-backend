using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("productcategories")]
public class ProductCategorie
{
    [Key]
    [Column("productcategorieid")]
    public int ProductCategorieId {get;set;}

    [Column("category")]
    public string Category {get; set;} = String.Empty;
}
