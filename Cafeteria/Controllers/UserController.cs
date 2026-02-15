using AutoMapper;
using Cafeteria.Models.Dtos.User;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            var usersDto = new List<UserResponseDto>();
            foreach(var user in users)
            {
                usersDto.Add(_mapper.Map<UserResponseDto>(user));
            }
            return Ok(usersDto);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser([FromRoute] int id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest(ModelState);
            }

            /* if (!_userRepository.IsUniqueUser(createUserDto.Email))
            {
                ModelState.AddModelError("CustomError", "El email ya existe");
                return BadRequest(ModelState);
            } */

            var user = await _userRepository.Register(createUserDto);
            if (user == null)
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al registrar el usuario");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetUser", new { id = user.UserId }, user);
        }
    
        [HttpPatch("updateUser/{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (id <= 0)
            {
                return BadRequest("El id del usuario no es válido");
            }

            if (userUpdateDto == null)
            {
                return BadRequest("Los datos del usuario no pueden estar vacíos");
            }

            var userFound = _userRepository.GetUser(id);
            if (userFound == null)
            {
                return NotFound("El usuario a actualizar no existe");
            }

            // Mapear el DTO a la entidad User
            var user = _mapper.Map<User>(userUpdateDto);
            user.UserId = id;

            var userUpdated = await _userRepository.Update(user);

            if (userUpdated != null)
            {
                return Ok(new { message = "Usuario actualizado exitosamente", user = userUpdated });
            }

            return StatusCode(500, "Algo salió mal al actualizar el usuario");
        }
    
        

    }
}