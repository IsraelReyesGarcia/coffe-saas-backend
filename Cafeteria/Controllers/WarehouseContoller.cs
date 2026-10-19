using AutoMapper;
using Cafeteria.Models.Dtos.Warehouse;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseContoller : ControllerBase
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IMapper _mapper;

    public WarehouseContoller(IWarehouseRepository warehouseRepository, IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult GetWarehouses()
    {
        var warehouses = _warehouseRepository.GetWarehouses();
        if(warehouses == null)
        {
            return BadRequest($"Algo salió mal al obtener los almacenes");
        }

        var warehousesDto = _mapper.Map<List<WarehouseDto>>(warehouses);

        return Ok(warehousesDto);
    }

    [HttpGet("{id:int}", Name = "getWarehouse")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult GetWarehouse(int id)
    {
        if(id <= 0)
        {
            ModelState.AddModelError("CustomeError", $"Envía un id valido");
            return BadRequest(ModelState);
        }

        if (!_warehouseRepository.WarehouseExists(id))
        {
            return NotFound($"El almacen con el id {id} no existe");
        }

        var warehouse = _warehouseRepository.GetWarehouse(id);

        if(warehouse == null)
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener el almacen con id {id}");
            return BadRequest(ModelState); 
        }

        var warehuseDto = _mapper.Map<WarehouseDto>(warehouse);
        return Ok(warehuseDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public IActionResult CreateWarehouse(CreateWarehouseDto createWarehouseDto)
    {
        if(createWarehouseDto == null)
        {
            ModelState.AddModelError("CustomeError", $"Envía los datos correctos");
            return BadRequest(ModelState);
        }

        var warehouse = _mapper.Map<Warehouse>(createWarehouseDto);
        if (!_warehouseRepository.CreateWarehouse(warehouse))
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al crear el almacen");
            return BadRequest(ModelState);
        }

        var warehouseDto = _mapper.Map<WarehouseDto>(warehouse);
        return Ok(warehouseDto);
    }

    [HttpPatch("{id:int}", Name="updateWarehouse")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult UpdateWarehouse(int id, [FromBody] UpdateWarehouseDto updateWarehouseDto)
    {
        if(id <= 0)
        {
            ModelState.AddModelError("CustomeError", $"Envía un id valido");
            return BadRequest(ModelState);
        }

        if (!_warehouseRepository.WarehouseExists(id))
        {
            return NotFound($"El almacen con el id {id} no existe");
        }

        var warehouse = _warehouseRepository.GetWarehouse(id);

        if(warehouse == null)
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener el almacen con id {id}");
            return BadRequest(ModelState); 
        }

        if(updateWarehouseDto == null)
        {
            ModelState.AddModelError("CustomeError", $"Envía los datos correctos");
            return BadRequest(ModelState);
        }

         _mapper.Map(updateWarehouseDto,warehouse);

        if (!_warehouseRepository.UpdateWarehouse(warehouse))
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al actualizar el almacen");
            return BadRequest(ModelState);
        }

        var warehouseDto = _mapper.Map<WarehouseDto>(warehouse);
        return Ok(warehouseDto);
    }

    [HttpDelete("{id:int}", Name = "deleteWarehouse")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult DeleteWarehouse(int id)
    {
        if(id <= 0)
        {
            ModelState.AddModelError("CustomeError", $"Envía un id valido");
            return BadRequest(ModelState);
        }

        if (!_warehouseRepository.WarehouseExists(id))
        {
            return NotFound($"El almacen con el id {id} no existe");
        }

        var warehouse = _warehouseRepository.GetWarehouse(id);

        if(warehouse == null)
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al obtener el almacen con id {id}");
            return BadRequest(ModelState); 
        }

        if (!_warehouseRepository.DeleteWarehouse(warehouse))
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al eliminar el almacen con id {id}");
            return BadRequest(ModelState); 
        }

        return NoContent();
    }
    
}
