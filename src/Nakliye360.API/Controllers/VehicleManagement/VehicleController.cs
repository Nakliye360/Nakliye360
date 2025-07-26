using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.Application.Abstractions.Services.VehicleManagement;
using Nakliye360.Application.Models.DTOs.VehicleManagement;

namespace Nakliye360.API.Controllers.VehicleManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleDto>>> GetAll()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> Get(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateVehicleDto dto)
        {
            var id = await _vehicleService.CreateVehicleAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateVehicleDto dto)
        {
            var result = await _vehicleService.UpdateVehicleAsync(dto);
            if (result == "Vehicle not found") return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return NoContent();
        }
    }
}
