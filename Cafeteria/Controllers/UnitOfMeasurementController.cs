using AutoMapper;
using Cafeteria.Models.Dtos.UnitOfMeasurement;
using Cafeteria.Models.Dtos.Warehouse;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasurementController : ControllerBase
    {
        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;
        private readonly IMapper _mapper;

        public UnitOfMeasurementController(IUnitOfMeasurementRepository unitOfMeasurementRepository, IMapper mapper)
        {
            _unitOfMeasurementRepository = unitOfMeasurementRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetUnitOfMeasurements()
        {
            var units = _unitOfMeasurementRepository.GetUnitOfMeasurements();
            if(units == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener las unidades de medida");
                return BadRequest(ModelState);
            }

            var unitsDto = _mapper.Map<List<UnitOfMeasurementDto>>(units);

            return Ok(unitsDto);
        }

        [HttpGet("{id:int}", Name = "getUnitOfMeasurement")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult getUnitOfMeasurement(int id)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", $"Envía un id valido");
                return BadRequest(ModelState);
            }

            if (!_unitOfMeasurementRepository.UnitOfMeasurementExists(id))
            {
                return NotFound($"La unidad de medida con el id {id} no existe");
            }

            var unit = _unitOfMeasurementRepository.GetUnitOfMeasurement(id);

            if(unit == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener la unidad de medida con id {id}");
                return BadRequest(ModelState); 
            }

            var unitDto = _mapper.Map<UnitOfMeasurementDto>(unit);
            return Ok(unitDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public IActionResult CreateUnitOfMeasurement(CreateUnitOfMeasurementDto createUnitOfMeasurementDto)
        {
            if(createUnitOfMeasurementDto == null)
            {
                ModelState.AddModelError("CustomeError", $"Envía los datos correctos");
                return BadRequest(ModelState);
            }

            var unit = _mapper.Map<UnitOfMeasurement>(createUnitOfMeasurementDto);
            if (!_unitOfMeasurementRepository.CreateUnitOfMeasurement(unit))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al crear la unidad de medida");
                return BadRequest(ModelState);
            }

            var unitDto = _mapper.Map<UnitOfMeasurement>(unit);
            return Ok(unitDto);
        }

        [HttpPatch("{id:int}", Name="updateUnitOfMeasurement")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult UpdateUnitOfMeasurement(int id, [FromBody] UpdateUnitOfMeasurementDto updateUnitOfMeasurementDto)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", $"Envía un id valido");
                return BadRequest(ModelState);
            }

            if (!_unitOfMeasurementRepository.UnitOfMeasurementExists(id))
            {
                return NotFound($"La unidad de medida con el id {id} no existe");
            }

            var unit = _unitOfMeasurementRepository.GetUnitOfMeasurement(id);

            if(unit == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener la unidad de medida con id {id}");
                return BadRequest(ModelState); 
            }

            if(updateUnitOfMeasurementDto == null)
            {
                ModelState.AddModelError("CustomeError", $"Envía los datos correctos");
                return BadRequest(ModelState);
            }

            _mapper.Map(updateUnitOfMeasurementDto, unit);

            if (!_unitOfMeasurementRepository.UpdateUnitOfMeasurement(unit))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al actualizar la unidad de medida");
                return BadRequest(ModelState);
            }

            var unitDto = _mapper.Map<UnitOfMeasurementDto>(unit);
            return Ok(unitDto);
        }

        [HttpDelete("{id:int}", Name = "deleteUnitOfMeasurement")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult DeleteUnitOfMeasurement(int id)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", $"Envía un id valido");
                return BadRequest(ModelState);
            }

            if (!_unitOfMeasurementRepository.UnitOfMeasurementExists(id))
            {
                return NotFound($"La unidad de medida con el id {id} no existe");
            }

            var unit = _unitOfMeasurementRepository.GetUnitOfMeasurement(id);

            if(unit == null)
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener la unidad de medida con id {id}");
                return BadRequest(ModelState); 
            }

            if (!_unitOfMeasurementRepository.DeleteUnitOfMeasurement(unit))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al eliminar la unidad de medida con id {id}");
                return BadRequest(ModelState); 
            }

            return NoContent();
        }
    }
}
