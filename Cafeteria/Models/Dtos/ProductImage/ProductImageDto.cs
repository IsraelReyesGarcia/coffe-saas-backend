namespace Cafeteria.Models.Dtos.ProductImage;

public class ProductImageDto
{
    public int ProductImageId { get; set; }
    public int ProductId { get; set; }
    public string PathImage { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
