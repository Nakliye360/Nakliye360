using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.Application.Abstractions.Services.OrderManagement;
using Nakliye360.Application.Models.DTOs.OrderManagement;

namespace Nakliye360.API.Controllers.OrderManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateOrderDto dto)
        {
            var orderId = await _orderService.CreateOrderAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = orderId }, null);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateOrderDto dto)
        {
            var result = await _orderService.UpdateOrderAsync(dto);
            if (result == "Order not found") return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
