using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Table;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TableController : ControllerBase
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public TableController(ITableRepository tableRepository, IMapper mapper)
    {
      _tableRepository = tableRepository;
      _mapper = mapper;  
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult GetTables()
    {
        var tables = _tableRepository.getTables();
        return Ok(tables);
    }

    [HttpGet("{id:int}", Name ="GetTable")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult GetTable(int id)
    {
        var table = _tableRepository.getTable(id);
        if(table == null)
        {
            return NotFound();
        }

        return Ok(table);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public async Task<IActionResult> CreateTable([FromBody] CreateTableDto createTableDto)
    {
        if(createTableDto == null)
        {
            return BadRequest(ModelState);
        }

        var table = await _tableRepository.CreateTable(createTableDto);
        
        if(table == null)
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al registrar tu mesa");
            return BadRequest(ModelState);
        }

        return Ok(table);
    }   

    [HttpPatch("{id:int}", Name ="UpdateTable")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<IActionResult> UpdateTable(int id, [FromBody] TableDto tableDto)
    {
        if(tableDto == null)
        {
            return BadRequest(ModelState);
        }

        if (!_tableRepository.TableExist(id))
        {
            return NotFound();
        }

        var table = await _tableRepository.UpdateTable(tableDto);

        if(table == null)
        {
            ModelState.AddModelError("CustomeError",$"Algo salió mal al actualizar los datos de tu mesa");
            return BadRequest(ModelState);
        }
        return Ok(table);
    }

    [HttpDelete("{id:int}",Name ="DeleteTable")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public IActionResult DeleteTable(int id)
    {
        if(id == 0)
        {
            return BadRequest(ModelState);
        }

        var tableDto = _tableRepository.getTable(id);
        if(tableDto == null)
        {
            return NotFound();
        }

        if (!_tableRepository.DeleteTable(tableDto))
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al eliminar la mesa"); 
        }
        return NoContent();
    }

}
