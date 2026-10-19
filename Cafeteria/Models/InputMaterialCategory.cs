using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("inputmaterialcategories")]
public class InputMaterialCategory
{
    [Key]
    [Column("inputmaterialcategoryid")]
    public int InputMaterialCategoryId { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("suggestionusagetype")]
    public int SuggestionUssageType { get; set; }
}
