using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.ProductImage;
using Cafeteria.Repository.IRepository;
using Cafeteria.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace Cafeteria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductImageController : ControllerBase
{
    private readonly IProductImageRepository _productImageRepository;
    private readonly IProductImageService _productImageService;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private const long MaxBytes = 5 * 1024 * 1024; // 5 MB
    private readonly ILogger<ProductImageController> _logger;

    public ProductImageController(
        IProductImageRepository productImageRepository,
        IProductImageService productImageService,
        IProductRepository productRespository,
        IMapper mapper,
        ILogger<ProductImageController> logger)
    {
        _productImageRepository = productImageRepository;
        _productImageService = productImageService;
        _productRepository = productRespository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult GetProductImages()
    {
        var productImages = _productImageRepository.GetProductImages();
        var productImagesDto = new List<ProductImageDto>();
        foreach (var productImage in productImages)
        {
            productImagesDto.Add(_mapper.Map<ProductImageDto>(productImage));
        }
        return Ok(_productImageService.GetAll());
    }

    [HttpGet("{id:int}", Name = "GetProductImage")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult GetProductImage(int id)
    {
        var productImage = _productImageRepository.GetProductImages(id);

        if (productImage == null)
        {
            return NotFound($"La imégen con el Id {id} no existe");
        }

        var productImageDto = _productImageService.GetById(id);
        if (productImageDto == null)
            return NotFound($"La imagen con el Id {id} no existe");
        return Ok(productImageDto);
    }

    /* Obtener lista de imágenes por producto */
    [HttpGet("/api/products/{productId:int}/images", Name = "GetImagesByProduct")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]   
    public IActionResult GetImagesByProduct(int productId)
    {
        if (!_productRepository.ProductExist(productId))
            return NotFound($"El producto con el id {productId} no existe");

        return Ok(_productImageService.GetByProduct(productId));
    }

    [HttpPost("/api/products/{productId:int}/images", Name = "CreateProductImage")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(MaxBytes + 64 * 1024)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> CreateProductImage(
        int productId,
        [FromForm] CreateImageDto createImageDto,
        CancellationToken cancellationToken

        )
    {
        // 1. ¿Existe el producto?
        if (!_productRepository.ProductExist(productId))

        {
           return NotFound($"El producto con el id {productId} no existe");
        }

        // 2. ¿Viene un archivo con contenido y de máximo 5 MB?
        var file = createImageDto.File;
        if (file == null || file.Length == 0)

        {
            ModelState.AddModelError("CustomError", "Envía una imagen");
            return BadRequest(ModelState);
        }

        if (file.Length > MaxBytes)
        {
            ModelState.AddModelError("CustomError", "La imagen debe pasar pesar máximo 5MB");
            return BadRequest(ModelState);
        }

        // 3. Delegar al servicio (procesa, sube a RustFS y guarda en la BD)
        try
        {
            var productImageDto = await _productImageService.CreateAsync(productId, file, cancellationToken);
            return CreatedAtRoute("GetProductImage", new { id = productImageDto.ProductImageId }, productImageDto);

        }
        catch (ImageFormatException)
        {
            ModelState.AddModelError("CustomError", "El archivo no es una imagen válida (JPEG, PNG o WEBP)");
            return BadRequest(ModelState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir la imagen del producto {ProductId}", productId);
            ModelState.AddModelError("CustomError", "Ocurrió un error al guardar la imagen, inténtalo nuevamente");
            return StatusCode(500, ModelState);
        }


    }

    
}
