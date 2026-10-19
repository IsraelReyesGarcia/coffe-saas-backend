using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cafeteria.Repository.IRepository;
using AutoMapper;
using Cafeteria.Models.Dtos;
using Cafeteria.Models;
using Cafeteria.Models.Enums;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetRoles()
        {
            var roles = _roleRepository.GetRoles();
            var rolesDto = new List<ApplicationRole>();
            /* foreach(var role in roles)
            {
                rolesDto.Add(_mapper.Map<RoleDto>(role));
            } */

            return Ok(rolesDto);
        }

        [HttpGet("{id}", Name = "GetRole")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await _roleRepository.GetRole(id);
            if(role == null)
            {
                return NotFound($"El role con el id {id} no existe");
            }

            //var roleDto = _mapper.Map<RoleDto>(role); /* _mapper.Map<Donde se va a mapear>(Lo que se está mapeando) */
            return Ok(_mapper.Map<RoleDto>(role));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            if(createRoleDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_roleRepository.RoleExists(createRoleDto.Name))
            {
                ModelState.AddModelError("CustomeError", "El rol ya existe");
                return BadRequest(ModelState);
            }

            var role = _mapper.Map<ApplicationRole>(createRoleDto);
            if (!await _roleRepository.CreateRole(role))
            {
                ModelState.AddModelError("CustomError",$"Algo salió mal al guardar el registro {role.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetRole", new {id=role.Id}, _mapper.Map<RoleDto>(role));
        }


    }
}
