using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("input_material")]
public class InputMaterial
{
    [Key]
    [Column("inputmaterialid")]
    public int InputMaterialId { get; set; }

    [Column("code")]
    public string Code { get; set; } = string.Empty;

    [Column("Name")]
    public string Name { get; set; } = string.Empty;

    [Column("saleprice")]
    public decimal SalePrice { get; set; }

    [Column("purchasePrice")]
    public decimal PuchasePrice { get; set; }

    [Column("averageduration")]
    public int AverageDuration { get; set; }

    [Column("inputmaterialcategoryId")]
    public int InputMaterialCategoryId { get; set; }

    [ForeignKey(nameof(InputMaterialCategoryId))]
    public required InputMaterialCategory InputMaterialCategory { get; set; }
}
