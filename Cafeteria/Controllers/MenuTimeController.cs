using AutoMapper;
using Cafeteria.Models.Dtos.MenuTime;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuTimeController : ControllerBase
    {
        private readonly IMenuTimeRepository _menuTimeRepository;
        private readonly IMapper _mapper;

        public MenuTimeController(IMenuTimeRepository menuTimeRepository, IMapper mapper)
        {
            _menuTimeRepository = menuTimeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetMenuTimes()
        {
            var menuTimes = _menuTimeRepository.GetMenuTimes();
            var menuTimesDto = _mapper.Map<List<MenuTimeDto>>(menuTimes);

            return Ok(menuTimesDto);
        }

        [HttpGet("{id:int}", Name="GetMenuTime")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetMenuTime(int id)
        {
            if (!_menuTimeRepository.MenuTimeExists(id))
            {
                return NotFound($"El tiempo de menú con id {id} no existe");
            }

            var menuTime = _menuTimeRepository.GetMenuTime(id);
            if(menuTime == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo ocurrió mal al obtener el registro de la base de datos");
                return BadRequest(ModelState);
           }

            var menuTimeDto = _mapper.Map<MenuTimeDto>(menuTime);

            return Ok(menuTimeDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public IActionResult CreateMenuTime([FromBody] CreateMenuTimeDto createMenuTimeDto)
        {
            if(createMenuTimeDto == null){
                return BadRequest($"Envía los datos correctos");
            }

            var menuTime = _mapper.Map<MenuTime>(createMenuTimeDto);

            if (!_menuTimeRepository.CreateMenuTime(menuTime))
            {
                return BadRequest($"Error al crear el tiempo de menú");
            }

            var menuTimeDto = _mapper.Map<MenuTimeDto>(menuTime);
            return Ok(menuTimeDto);
        }

        [HttpPatch("{id:int}",Name="UpdateMenuTime")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult UpdateMenuTime(int id, [FromBody] UpdateMenuTimeDto updateMenuTimeDto)
        {
            if (id <= 0)
            {
                return BadRequest($"Ingrese un id valido");
            }

            if (!_menuTimeRepository.MenuTimeExists(id))
            {
                return NotFound($"El tiempo de menú con el id {id} no existe");
            }

            if(updateMenuTimeDto == null){
                return BadRequest($"Ingrese los datos correctos");
            }

            var updateMenuTime = _menuTimeRepository.GetMenuTime(id);
            if(updateMenuTime == null)
            {
                return BadRequest($"Error obtener el tiempo de menú, intente de nuevo");
            }

            _mapper.Map(updateMenuTimeDto,updateMenuTime);

            if (!_menuTimeRepository.UpdateMenuTime(updateMenuTime))
            {
                return BadRequest($"Error al actualizar el tiempo de menú, el formato de hora debe ser hh:mm:ss");
            }

            return Ok(updateMenuTimeDto);
        }

        [HttpDelete("{id:int}", Name="DeleteMenuTime")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult DeleteMenuTime(int id)
        {
            if(id <= 0)
            {
                return BadRequest($"Ingresa un id valido");
            }

            if (!_menuTimeRepository.MenuTimeExists(id))
            {
                return BadRequest($"El tiempo de menú con id {id} no existe");
            }

            var menuTime = _menuTimeRepository.GetMenuTime(id);
            
            if(menuTime == null)
            {
                return BadRequest($"Error al obtener el tiempo de menú en la base de datos, vuelva a intentar");
            }

            if (!_menuTimeRepository.DeleteMenuTime(menuTime))
            {
                ModelState.AddModelError("CustomeError",$"Error al eliminar el tiempo de menú, intente de nuevo");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
