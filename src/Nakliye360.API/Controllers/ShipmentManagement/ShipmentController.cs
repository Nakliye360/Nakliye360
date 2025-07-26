using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.Application.Abstractions.Services.ShipmentManagement;
using Nakliye360.Application.Models.DTOs.ShipmentManagement;

namespace Nakliye360.API.Controllers.ShipmentManagement
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _shipmentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var shipment = await _shipmentService.GetByIdAsync(id);
            return shipment == null ? NotFound() : Ok(shipment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var id = await _shipmentService.CreateShipmentAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateShipmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var message = await _shipmentService.UpdateShipmentAsync(dto);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shipmentService.DeleteShipmentAsync(id);
            return NoContent();
        }
    }
}