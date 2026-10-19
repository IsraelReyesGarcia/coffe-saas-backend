using AutoMapper;
using Cafeteria.Models.Dtos.ProductImage;
using Cafeteria.Repository.IRepository;
using Cafeteria.Services.IServices;
using Cafeteria.Models;

namespace Cafeteria.Services;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _productImageRepository;
    private readonly IImageProcessor _imageProcessor;
    private readonly IObjectStorage _storage;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductImageService> _logger;

    public ProductImageService(
        IProductImageRepository productImageRepository,
        IImageProcessor imageProcessor,
        IObjectStorage storage,
        IMapper mapper,
        ILogger<ProductImageService> logger)
    {
        _productImageRepository = productImageRepository;
        _imageProcessor = imageProcessor;
        _storage = storage;
        _mapper = mapper;
        _logger = logger;
    }

    // Convierte la entidad en DTO con la URL Pública
    private ProductImageDto ToDto(ProductImage productImage)
    {
        var productImageDto = _mapper.Map<ProductImageDto>(productImage);
        productImageDto.PathImage = _storage.GetPublicUrl(productImageDto.PathImage);
        return productImageDto;
    }

    public ICollection<ProductImageDto> GetAll()
    {
        return _productImageRepository.GetProductImages().Select(ToDto).ToList();
    }

    public ProductImageDto? GetById(int productImageId)
    {
        var productImage = _productImageRepository.GetProductImages(productImageId);
        return productImage == null ? null : ToDto(productImage);
    }

    public ICollection<ProductImageDto> GetByProduct(int productId)
    {
        return _productImageRepository.GetImagesByProduct(productId).Select(ToDto).ToList();
    }

    public async Task<ProductImageDto> CreateAsync(int productId, IFormFile file, CancellationToken cancellationToken)
    {
        // 1. Procesar (lanza ImageFormatException si no es una imagen válida)
        await using var input = file.OpenReadStream();
        var processed = await _imageProcessor.ProcessAsync(input, cancellationToken);

        // 2. Subir a RustFS con una ruta generada por el servidor
        var key = $"products/{productId}/{Guid.NewGuid()}.webp";
        await using (processed.Content)
        {
            await _storage.UploadAsync(key, processed.Content, "image/webp", cancellationToken);
        }

        // 3. Guardar el registro; si falla, borrar el objeto para no dejar huérfanos
        var originalName = Path.GetFileName(file.FileName);
        var productImage = new ProductImage
        {
            ProductId = productId,
            PathImage = key,
            Width = processed.Width,
            Height = processed.Height,
            OriginalFileName = originalName.Length > 255 ? originalName[..255] : originalName,
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            if (!_productImageRepository.CreateProductImage(productImage))
            {
                throw new InvalidOperationException("No se pudo guardar el registro de la imagen");
            }
        }
        catch
        {
            await _storage.DeleteAsync(key, CancellationToken.None);
            throw;
        }

        // 4. Responder con la URL pública
        var productImageDto = _mapper.Map<ProductImageDto>(productImage);
        productImageDto.PathImage = _storage.GetPublicUrl(key);
        return productImageDto;
    }

    public async Task<bool> DeleteAsync(int productImageId, CancellationToken cancellationToken)
    {
        var productImage = _productImageRepository.GetProductImages(productImageId);
        if (productImage == null) return false;

        var key = productImage.PathImage;
        if (!_productImageRepository.DeleteProductImage(productImage)) return false;

        // Primero BD, después RusFS: un objeto huérfano es menos grave que un registro roto
        try
        {
            await _storage.DeleteAsync(key, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "No se pudo borrar el objeto {key} de RustFS", key);
        }
        return true;
    }
}
