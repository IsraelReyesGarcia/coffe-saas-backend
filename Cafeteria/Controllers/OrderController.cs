using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Order;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrders()
        {
            var orders = _orderRepository.GetOrders();
            var ordersDto = _mapper.Map<List<OrderDto>>(orders);

            return Ok(ordersDto);
        }

        [HttpGet("{id:int}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrder(int id)
        {
            if (!_orderRepository.OrderExists(id))
            {
                return NotFound($"La orden con id {id} no existe.");
            }

            var order = _orderRepository.GetOrder(id);
            if (order == null)
            {
                return BadRequest(ModelState);
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
            {
                ModelState.AddModelError("CustomeError", "Envié los datos de la orden correctamente");
                return BadRequest(ModelState);
            }

            var order = _mapper.Map<Order>(createOrderDto);

            if (!_orderRepository.CreateOrder(order))
            {
                ModelState.AddModelError("CustomeError", "Error al crear la orden");
                return StatusCode(500, ModelState);
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpPatch("{id:int}/cancel", Name = "CancelOrder")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CancelOrder(int id, [FromBody] CancelOrderDto cancelOrderDto)
        {
            if (!_orderRepository.OrderExists(id))
            {
                return NotFound($"No se encontró la orden {id}");
            }

            var order = _orderRepository.GetOrder(id);
            if (order == null)
            {
                return BadRequest(ModelState);
            }

            var orderStatus = order.Status;
            switch (orderStatus)
            {
                case 1: break;
                case 2: return BadRequest("No se puede volver a cancelar una orden cancelada.");
                case 3: return BadRequest("No se puede cancelar una orden que ya ha sido pagada.");
                default: return BadRequest($"El status {orderStatus} no tiene un valor asociado.");
            }

            _mapper.Map(cancelOrderDto, order);

            if (!_orderRepository.CancelOrder(order))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al cancelar la orden {id}");
                return BadRequest(ModelState);
            }

            var orderCanceledDto = _mapper.Map<OrderDto>(order);
            return Ok(orderCanceledDto);
        }
        
        [HttpPatch("{id:int}/inPreparation", Name="MarkInPreparation")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MarkInPreparation(int id)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", "Ingresa un estado de orden váido");
                return BadRequest(ModelState);
            }

            if (!_orderRepository.OrderExists(id))
            {
                return NotFound($"No se encontró la orden con el id: {id}");
            }

            var order = _orderRepository.GetOrder(id);
            if(order == null)
            {
                return StatusCode(500,"Error al obtener tu orden de la base de datos, intentelo nuevamente");
            }

            //Validar el status
            switch (order.Status)
            {
                case 1: break;
                case 2: return BadRequest("Esta orden ya ha sido marcada en preparación");
                case 3: return BadRequest("Esta orden ya ha sido marcada como lista");
                case 4: return BadRequest("Esta orden ya ha sido marcada como entregada");
                case 5: return BadRequest("Esta orden ya ha sido marcada como pagada");
                case 99: return BadRequest("No se puede marcar en preparada un orden cancelada");
                default: return BadRequest($"El status ${order.Status} no tiene un valor asociado");
            }

            if (!_orderRepository.MarkInPreparation(order))
            {
                return BadRequest("Error al tratar de marcar orden en preparación, intentelo nuevamente");
            }

            return NoContent();
            
        }

        [HttpPatch("{id:int}/ready", Name="IsReady")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult IsReady(int id)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", "Ingresa un estado de orden váido");
                return BadRequest(ModelState); 
            }

            if (!_orderRepository.OrderExists(id))
            {
                return NotFound($"La orden con id {id} no existe");
            }

            var order = _orderRepository.GetOrder(id);
            if(order == null)
            {
                return StatusCode(500,"Error al obtener tu orden de la base de datos, intentelo nuevamente");
            }

            //Validar el status
            switch (order.Status)
            {
                case 1: return BadRequest("No puede marcar como lista una orden que está en estado creada");
                case 2: break;
                case 3: return BadRequest("Esta orden ya ha sido marcada como lista");
                case 4: return BadRequest("Esta orden ya ha sido marcada como entregada");
                case 5: return BadRequest("Esta orden ya ha sido marcada como pagada");
                case 99: return BadRequest("No se puede marcar en preparada un orden cancelada");
                default: return BadRequest($"El status ${order.Status} no tiene un valor asociado");
            }

            if (!_orderRepository.MarkReady(order))
            {
                ModelState.AddModelError("CustomeError", $"Ocurrió un error al marcar tu orden como lista, intentelo nuevamente");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
        
        [HttpPatch("{id:int}/delivered", Name="IsDelivered")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult IsDelivered(int id)
        {
            if(id <= 0)
            {
                ModelState.AddModelError("CustomeError", $"Ingresa un id valido");
                return BadRequest(ModelState);
            }

            if (!_orderRepository.OrderExists(id))
            {
                return NotFound($"No se encontró una orden con el id {id}");
            }

            var order = _orderRepository.GetOrder(id);
            if(order == null)
            {
                return StatusCode(500,"Error al obtener tu orden de la base de datos, intentelo nuevamente");
            }

            //Validación de estado
            switch (order.Status)
            {
                case 1: return BadRequest("No puede marcar como lista una orden que está en estado creada");
                case 2: return BadRequest("Esta orden ya ha sido marcada como lista");
                case 3: break;
                case 4: return BadRequest("Esta orden ya ha sido marcada como entregada");
                case 5: return BadRequest("Esta orden ya ha sido marcada como pagada");
                case 99: return BadRequest("No se puede marcar en preparada un orden cancelada");
                default: return BadRequest($"El status ${order.Status} no tiene un valor asociado");
            }

            if (!_orderRepository.MarkDelivered(order))
            {
                ModelState.AddModelError("CustomeError", $"Error al marcar tu orden como enviada");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPatch("{id:int}/pay", Name="ApproveOrder")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ApproveOrder(int id, [FromBody] PayOrderDto payOrderDto)
        {
            if (id <= 0)
            {
                return BadRequest("Ingrese un id valido");
            }

            if (!_orderRepository.OrderExists(id))
            {
                return NotFound($"No se encontró la orden con el id {id}");
            } 

            var order = _orderRepository.GetOrder(id);
            if(order == null)
            {
                return BadRequest("Error al obtener la orden de la base de datos");
            }

            var orderStatus = order.Status;
            //Validar el status
            switch (orderStatus)
            {
                case 1: return BadRequest("No se puede pagar una orden que solo ha sido creada, necesitas pasar el flujo completo");
                case 2: return BadRequest("No se puede pagar una orden en preparación");
                case 3: return BadRequest("No se puede pagar una orden lista");
                case 4: break;
                case 5: return BadRequest("Esta orden ya ha sido marcada como pagada");
                case 99: return BadRequest("No se puede pagar una orden cancelada");
                default: return BadRequest($"El status ${orderStatus} no tiene un valor asociado");
            }

            _mapper.Map(payOrderDto, order);

            if (!_orderRepository.PayOrder(order))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al aprobar la orden, intente de nuevo");
                return BadRequest(ModelState);
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

    }
}
