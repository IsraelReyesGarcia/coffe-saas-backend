using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Cafeteria.Models.Dtos.Product;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Responses;
using Cafeteria.Services.IServices;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        private readonly IObjectStorage _storage;

        public ProductController(IProductRepository productRepository, IMapper mapper, IObjectStorage storage)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _storage = storage;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("/api/Product/Paged", Name="GetProductsInPage")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetProductsInPage(
            [FromQuery] int pageNumber=1, 
            [FromQuery] int pageSize=10,
            [FromQuery] string? searchText = null
            )
        {
            if(pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Los parámetros de paginación no son validos");
            }
            
            var totalProducts = _productRepository.GetTotalProducts(searchText);
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            if (pageNumber > totalPages)
            {
                return NotFound("No hay más paginas disponibles");
            }
            var products = _productRepository.GetProductsInPages(pageNumber, pageSize, searchText);
            foreach(var product in products)
            {
                if (product.CoverImage != null)
                    product.CoverImage = _storage.GetPublicUrl(product.CoverImage);
            }
            //var productDto = _mapper.Map<List<ProductDto>>(products);
            var paginationResponse = new PaginationResponse<ProductResponseDto>
            {
              PageNumber = pageNumber,  
              PageSize = pageSize,
              Total = totalProducts,
              TotalPages = totalPages,
              Data = products
            };

            return Ok(paginationResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> CreateProduct([FromBody]CreateProductDto createProductDto)
        {
            if(createProductDto == null)
            {
                return BadRequest(ModelState);
            }

            var product = await  _productRepository.Register(createProductDto);
            if(product == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al registrar el producto.");
            };
            return Ok(product);
        } 
    }
}