using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cafeteria.Repository.IRepository;
using AutoMapper;
using Cafeteria.Models.Dtos;
using Cafeteria.Models;

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
            var roles = _roleRepository.getRoles();
            var rolesDto = new List<RoleDto>();
            foreach(var role in roles)
            {
                rolesDto.Add(_mapper.Map<RoleDto>(role));
            }

            return Ok(rolesDto);
        }

        [HttpGet("{id:int}", Name = "GetRole")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetRole(int id)
        {
            var role = _roleRepository.GetRole(id);
            if(role == null)
            {
                return NotFound($"El role con el id {id} no existe");
            }

            var roleDto = _mapper.Map<RoleDto>(role); /* _mapper.Map<Donde se va a mapear>(Lo que se está mapeando) */
            return Ok(roleDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            if(CreateRole == null)
            {
                return BadRequest(ModelState);
            }

            if (_roleRepository.RoleExists(createRoleDto.Name))
            {
                ModelState.AddModelError("CustomeError", "El rol ya existe");
                return BadRequest(ModelState);
            }

            var role = _mapper.Map<Role>(createRoleDto);
            if (!_roleRepository.CreateRole(role))
            {
                ModelState.AddModelError("CustomError",$"Algo salió mal al guardar el registro {role.name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetRole", new {id=role.roleid}, role);
        }


    }
}
