using AutoMapper;
using Cafeteria.Models.Dtos.ProductCategorie;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategorieController : ControllerBase
    {
        private readonly IProductCategorieRepository _productCategorieRepository;
        private readonly IMapper _mapper;

        public ProductCategorieController(IProductCategorieRepository productCategorieRepository, IMapper mapper)
        {
            _productCategorieRepository = productCategorieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetProductCategory()
        {
            var productCategory = _productCategorieRepository.GetProductCategories();
            return Ok(productCategory);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategorieDto createProductCategorieDto)
        {
            if(createProductCategorieDto == null)
            {
                return BadRequest(ModelState);
            }

            var productCategory = await _productCategorieRepository.Register(createProductCategorieDto);
            if(productCategory == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al registrar la categoria de producto");
            }

            return Ok(productCategory);
        }

        [HttpPatch("UpdateProductCategory/{id:int}",Name ="UpdateProductCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProductCategory(int id, [FromBody] ProductCategorieDto productCategorieDto)
        {
            if(productCategorieDto == null)
            {
                return BadRequest("Envía datos validos");
            }

            var productCategory = await _productCategorieRepository.Update(productCategorieDto);
            if(productCategory != null)
            {
                return Ok(new {message ="Categoria actualizada correctamente",productCategory = productCategory});
            }
            return StatusCode(500, "Algo salió mal al actualizar la categoría");
        }
    
        [HttpDelete("{productCategoryId:int}", Name ="DeleteProductCategory")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult DeleteProductCategory(int productCategoryId)
        {
            if(productCategoryId == 0)
            {
                return BadRequest(ModelState);
            }

            var productCategoryDto = _productCategorieRepository.GetProductCategorie(productCategoryId);
            if(productCategoryDto == null)
            {
                return NotFound($"La categoría de producto con el id {productCategoryId} no existe");
            }

            if (!_productCategorieRepository.Delete(productCategoryDto))
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al eliminar la categoría");
                return BadRequest(ModelState);
            }

            return NoContent();

        }
    }
}
