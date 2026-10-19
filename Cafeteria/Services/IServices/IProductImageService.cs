using Cafeteria.Models.Dtos.ProductImage;

namespace Cafeteria.Services.IServices;

public interface IProductImageService
{
    ICollection<ProductImageDto> GetAll();
    ProductImageDto? GetById(int productImageId);
    ICollection<ProductImageDto> GetByProduct(int productId);
    Task<ProductImageDto> CreateAsync(int productId, IFormFile file, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int productImageId, CancellationToken cancellationToken);
}
