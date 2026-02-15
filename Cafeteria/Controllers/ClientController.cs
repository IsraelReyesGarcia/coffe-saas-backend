using AutoMapper;
using Cafeteria.Models.Dtos.Client;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cafeteria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRespository;
    private readonly IMapper _mapper;

    public ClientController(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRespository = clientRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public IActionResult GetClients()
    {
        var clients = _clientRespository.GetClients();
        return Ok(clients);
    }

    [HttpGet("{id:int}", Name="GetClient")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult getClient(int id){
        
        if(!_clientRespository.ClientExist(id)){
            return BadRequest(ModelState);
        }

        var client = _clientRespository.GetClient(id);
        return Ok(client);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public async Task<IActionResult> CreateClient([FromBody] CreateClientDto createClientDto)
    {
        if(createClientDto == null)
        {
            return BadRequest(ModelState);
        }

        var client = await _clientRespository.CreateClient(createClientDto);
        if(client == null)
        {
            ModelState.AddModelError("CustomeError", $"Algo salió mal al registrar tu cliente");
            return BadRequest(ModelState);
        }

        return Ok(client);
    }

    [HttpPatch("{id:int}", Name="UpdateClient")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<IActionResult> UpdateClient(int id, [FromBody] UpdateClientDto updateClientDto)
    {
        if(updateClientDto == null){
            return BadRequest(ModelState);
        }

        if(!_clientRespository.ClientExist(id)){
            return NotFound();
        }

        var client = await _clientRespository.UpdateClient(updateClientDto);
        if(client == null){
            ModelState.AddModelError("CustomeError", $"Algo salió mal al actualizar tu cliente");
            return BadRequest(ModelState);
        }

        return Ok(client);
    }

    [HttpDelete("{id:int}", Name = "DeleteClient")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public IActionResult DeleteClient(int id){
        if(id <= 0){
            return BadRequest(ModelState);
        }

        var clientDto = _clientRespository.GetClient(id);
        if(clientDto == null){
            return NotFound();
        }

        if(!_clientRespository.DeleteClient(clientDto)){
            ModelState.AddModelError("CustomeError",$"Algo salió mal al eliminar tu cliente");
            return BadRequest(ModelState);
        }   

        return NoContent();
    }
}
