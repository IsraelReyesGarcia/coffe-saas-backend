using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Dtos.ProductImage;

public class CreateImageDto
{
    [Required]
    public IFormFile File { get; set; } = null!;
}
