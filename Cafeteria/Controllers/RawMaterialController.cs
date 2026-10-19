using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Cafeteria.Models.Dtos.RawMaterial;
using Cafeteria.Models.Dtos.Warehouse;
using Cafeteria.Models;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RawMaterialController : ControllerBase
    {
        private readonly IRawMaterialRepository _rawMaterialRepository;
        private readonly IMapper _mapper;

        public RawMaterialController(IRawMaterialRepository rawMaterialRepository, IMapper mapper)
        {
            _rawMaterialRepository = rawMaterialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetRawMaterials()
        {
            var rawMaterials = _rawMaterialRepository.GetRawMaterials();
            if(rawMaterials == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener las materias primas");
                return BadRequest(ModelState);
            }

            var rawMaterialsDto = _mapper.Map<List<RawMaterialDto>>(rawMaterials);
            return Ok(rawMaterialsDto);
        }

        [HttpGet("{id:int}", Name = "getRawMaterial")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetRawMaterial(int id)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", $"Envía un id valido");
                return BadRequest(ModelState);
            }

            if (!_rawMaterialRepository.RawMaterialExists(id))
            {
                return NotFound($"La materia prima con el id {id} no existe");
            }

            var rawMaterial = _rawMaterialRepository.GetRawMaterial(id);

            if(rawMaterial == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener la materia prima con id {id}");
                return BadRequest(ModelState); 
            }

            var rawMaterialDto = _mapper.Map<RawMaterialDto>(rawMaterial);
            return Ok(rawMaterialDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public IActionResult CreateRawMaterial(CreateRawMatetialDto createRawMatetialDto)
        {
            if(createRawMatetialDto == null)
            {
                ModelState.AddModelError("CustomeError", $"Envía los datos correctos");
                return BadRequest(ModelState);
            }

            var rawMaterial = _mapper.Map<RawMaterial>(createRawMatetialDto);
            if (!_rawMaterialRepository.CreateRawMaterial(rawMaterial))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al crear la materia prima");
                return BadRequest(ModelState);
            }

            var rawMaterialDto = _mapper.Map<RawMaterialDto>(rawMaterial);
            return Ok(rawMaterialDto);
        }

        [HttpPatch("{id:int}", Name="updateRawMaterial")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult UpdateRawMaterial(int id, [FromBody] UpdateRawMaterialDto updateRawMaterialDto)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", $"Envía un id valido");
                return BadRequest(ModelState);
            }

            if (!_rawMaterialRepository.RawMaterialExists(id))
            {
                return NotFound($"La materia prima con el id {id} no existe");
            }

            var rawMaterial = _rawMaterialRepository.GetRawMaterial(id);

            if(rawMaterial == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener la materia prima con id {id}");
                return BadRequest(ModelState); 
            }

            if(updateRawMaterialDto == null)
            {
                ModelState.AddModelError("CustomeError", $"Envía los datos correctos");
                return BadRequest(ModelState);
            }

            _mapper.Map(updateRawMaterialDto,rawMaterial);

            if (!_rawMaterialRepository.UpdateRawMaterial(rawMaterial))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al actualizar la materia prima");
                return BadRequest(ModelState);
            }

            var rawMaterialDto = _mapper.Map<RawMaterialDto>(rawMaterial);
            return Ok(rawMaterialDto);
        }

        [HttpDelete("{id:int}", Name = "deleteRawMaterial")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult DeleteRawMaterial(int id)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", $"Envía un id valido");
                return BadRequest(ModelState);
            }

            if (!_rawMaterialRepository.RawMaterialExists(id))
            {
                return NotFound($"La materia prima con el id {id} no existe");
            }

            var rawMaterial = _rawMaterialRepository.GetRawMaterial(id);

            if(rawMaterial == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener la materia priam con id {id}");
                return BadRequest(ModelState); 
            }

            if (!_rawMaterialRepository.DeleteRawMaterial(rawMaterial))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al eliminar la materia prima con id {id}");
                return BadRequest(ModelState); 
            }

            return NoContent();
        }
    }
}