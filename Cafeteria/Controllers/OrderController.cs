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

            _mapper.Map(cancelOrderDto, order);

            if (!_orderRepository.CancelOrder(order))
            {
                ModelState.AddModelError("CustomeError", $"Algo salió mal al cancelar la orden {id}");
                return BadRequest(ModelState);
            }

            var orderCanceledDto = _mapper.Map<OrderDto>(order);
            return Ok(orderCanceledDto);
        }
        
        [HttpPatch("{id:int}/approve", Name="ApproveOrder")]
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

            /* if (!_orderRepository.OrderExists(id))
            {
                return NotFound($"No se encontró la orden con el id {id}");
            } */

            var order = _orderRepository.GetOrder(id);
            if(order == null)
            {
                return BadRequest("Error al obtener la orden de la base de datos");
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
