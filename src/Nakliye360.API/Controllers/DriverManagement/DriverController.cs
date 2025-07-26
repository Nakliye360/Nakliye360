using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.Application.Abstractions.Services.DriverManagement;
using Nakliye360.Application.Models.DTOs.DriverManagement;

namespace Nakliye360.API.Controllers.DriverManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DriverDto>>> GetAll()
        {
            var drivers = await _driverService.GetAllAsync();
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> Get(int id)
        {
            var driver = await _driverService.GetByIdAsync(id);
            if (driver == null) return NotFound();
            return Ok(driver);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateDriverDto dto)
        {
            var id = await _driverService.CreateDriverAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateDriverDto dto)
        {
            var result = await _driverService.UpdateDriverAsync(dto);
            if (result == "Driver not found") return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _driverService.DeleteDriverAsync(id);
            return NoContent();
        }
    }
}
