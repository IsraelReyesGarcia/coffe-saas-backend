using AutoMapper;
using Cafeteria.Models.Dtos.InputMaterialCategory;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputMaterialCategoryController : ControllerBase
    {
        private readonly IInputMaterialCategoryRepository _inputMaterialCategoryRepository;
        private readonly IMapper _mapper;

        public InputMaterialCategoryController(IInputMaterialCategoryRepository inputMaterialCategoryRepository, IMapper mapper)
        {
            _inputMaterialCategoryRepository = inputMaterialCategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetInputCategories()
        {
            var categories = _inputMaterialCategoryRepository.GetInputCategories();
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetInputMaterialCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetInputMaterialCategory(int id)
        {
            if (id <= 0) return BadRequest();

            var category = _inputMaterialCategoryRepository.GetInputMaterialCategory(id);
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory([FromBody] InputMaterialCategoryDto inputMaterialCategoryDto)
        {
            if (inputMaterialCategoryDto == null) return BadRequest(ModelState);

            var created = await _inputMaterialCategoryRepository.CreateCategory(inputMaterialCategoryDto);
            if (created == null)
            {
                ModelState.AddModelError("CustomError", "Algo salió mal al crear la categoría.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetInputMaterialCategory", new { id = created.InputMaterialCategoryId }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] InputMaterialCategoryDto inputMaterialCategoryDto)
        {
            if (inputMaterialCategoryDto == null || id != inputMaterialCategoryDto.InputMaterialCategoryId)
                return BadRequest();

            var updated = await _inputMaterialCategoryRepository.UpdateCategory(inputMaterialCategoryDto);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCategory(int id)
        {
            if (id <= 0) return BadRequest();

            var category = _inputMaterialCategoryRepository.GetInputMaterialCategory(id);
            if (category == null) return NotFound();

            _inputMaterialCategoryRepository.DeleteCategory(category);
            return NoContent();
        }
    }
}
