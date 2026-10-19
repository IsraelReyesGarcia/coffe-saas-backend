using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Cafeteria.Models.Dtos.Menu;
using Cafeteria.Models;
using Mono.TextTemplating;
using Cafeteria.Constants;
using Cafeteria.Models.Dtos.Responses;
using Cafeteria.Models.Dtos.Responses;
using Cafeteria.Models.Dtos.Responses;

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
        //[ResponseCache(CacheProfileName = CacheProfiles.Default20)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetMenus([FromQuery] string? searchText = null)
        {
            System.Console.WriteLine($"Peticion de menus hecha el {DateTime.Now}");

            var menus = _menuRepository.GetMenus(searchText);
            if(menus == null)
            {
                return BadRequest($"Error al obtener los menus");
            }
            
            var menusDto = _mapper.Map<List<MenuDto>>(menus);

            System.Console.WriteLine("Respuesta correcta");

            return Ok(menusDto);
        }

        [HttpGet("/api/Menu/Paged", Name="GetMenusInPage")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetMenusInPage(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchText = null
        )
        {
            if(pageNumber < 0 && pageSize < 0)
            {
                return BadRequest("Los parámetros de paginación no son validos");
            }

            var totalMenus = _menuRepository.GetTotalMenus(searchText);
            var totalPages = (int)Math.Ceiling((double)totalMenus / pageSize);
            if(pageNumber > totalMenus)
            {
                return NotFound("No hay más páginas disponibles");
            }

            var menus = _menuRepository.GetMenusInPage(pageNumber, pageSize, searchText);

            var paginationResponse = new PaginationResponse<MenuResponseDto>
            {
              PageNumber = pageNumber,
              PageSize = pageSize,
              Total = totalMenus,
              TotalPages = totalPages,
              Data = menus
            };

            return Ok(paginationResponse);
        }

        [HttpGet("{id:int}", Name="GetMenu")]
        //[ResponseCache(Duration = 10)]
        [ResponseCache(CacheProfileName = CacheProfiles.Default10)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetMenu(int id)
        {
            System.Console.WriteLine($"Menú con el id {id} a las {DateTime.Now}");
            if(id <= 0)
            {
                return BadRequest($"Ingresa un id válido");
            }

            if (!_menuRepository.MenuExists(id))
            {
                return NotFound($"El menú con id {id} no existe");
            }

            var menu = _menuRepository.GetMenu(id);
            System.Console.WriteLine($"Respuesta con el id {id}");

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

            if (string.IsNullOrWhiteSpace(createMenuDto.Name))
            {
                ModelState.AddModelError("customeError", "Ingresa el nombre del menú para poder ser creado");
                return BadRequest(ModelState);
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
