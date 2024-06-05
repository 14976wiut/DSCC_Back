using AutoMapper;
using DSCC_Back.DTO;
using DSCC_Back.Interfaces;
using DSCC_Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSCC_Back.Controllers
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
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(orders));
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderReadDto>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderReadDto>(order));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrdersByUserId(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(orders));
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto)
        {
            var order = _mapper.Map<Order>(orderCreateDto);
            await _orderRepository.AddOrderAsync(order);

            var orderReadDto = _mapper.Map<OrderReadDto>(order);

            return CreatedAtRoute("GetOrderById", new { id = orderReadDto.OrderId }, orderReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _mapper.Map(orderUpdateDto, order);

            await _orderRepository.UpdateOrderAsync(order);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteOrderAsync(id);

            return NoContent();
        }
    }
}
