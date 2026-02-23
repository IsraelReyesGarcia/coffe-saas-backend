using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Cafeteria.Models.Dtos.Menu;
using Cafeteria.Models;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public MenuController(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetMenus()
        {
            var menus = _menuRepository.GetMenus();
            if(menus == null)
            {
                return BadRequest($"Error al obtener los menus");
            }
            
            var menusDto = _mapper.Map<List<MenuDto>>(menus);

            

            return Ok(menusDto);
        }

        [HttpGet("{id:int}", Name="GetMenu")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetMenu(int id)
        {
            if(id <= 0)
            {
                return BadRequest($"Ingresa un id válido");
            }

            if (!_menuRepository.MenuExists(id))
            {
                return NotFound($"El menú con id {id} no existe");
            }

            var menu = _menuRepository.GetMenu(id);

            if(menu == null)
            {
                return BadRequest($"Error al obtener el menú de la base de datos");
            }

            return Ok(menu);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult CreateMenu([FromBody] CreateMenuDto createMenuDto)
        {
            if(createMenuDto == null){
                return BadRequest("Ingrese los datos correctos");
            }

            var menu = _mapper.Map<Menu>(createMenuDto);

            if (!_menuRepository.CreateMenu(menu))
            {
                return StatusCode(500, $"Error al crear el menú");
            }

            var menuDto = _mapper.Map<MenuDto>(menu);

            return Ok(menuDto);
        }

        [HttpPatch("{id:int}", Name="updateMenu")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public IActionResult UpdateMenu(int id, [FromBody] UpdateMenuDto updateMenuDto)
        {
            if(id <= 0)
            {
                return BadRequest($"Ingrese un id correcto");
            }

            if(!_menuRepository.MenuExists(id))
            {
                return NotFound($"El menú con id {id} no existe");
            }

            var menu = _menuRepository.GetMenu(id);

            if(menu == null){
                return BadRequest($"Error al obtener el menú de la base de datos, intente nuevamente");
            }

            _mapper.Map(updateMenuDto,menu);

            if (!_menuRepository.UpdateMenu(menu))
            {
                return StatusCode(500, $"Error al eliminar el menú");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name="DeleteMenu")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult DeleteMenu(int id)
        {
            if(id <= 0)
            {
                return BadRequest($"Ingrese un id valido");
            }

            if (!_menuRepository.MenuExists(id))
            {
                return NotFound($"El menu con el id {id} no existe");
            }

            var menu = _menuRepository.GetMenu(id);

            if(menu == null){
                return BadRequest($"Error al obtener el menú, intente de nuevo");
            }

            if (!_menuRepository.DeleteMenu(menu))
            {
                return StatusCode(500, $"Error al eliminar el menú");
            }

            return NoContent();
        }
    }
}
