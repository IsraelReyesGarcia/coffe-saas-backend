using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("productimages")]
public class ProductImage
{
    [Key]
    [Column("productimageid")]
    public int ProductImageId { get; set; }

    [Column("productid")]
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    [MaxLength(200)]
    [Column("pathimage")]
    public string PathImage { get; set; } = string.Empty;

    [Column("createdat")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("width")]
    public int Width { get; set; }

    [Column("height")]
    public int Height { get; set; }

    [MaxLength(255)]
    [Column("originalfilename")]
    public string OriginalFileName { get; set; } = string.Empty;
}