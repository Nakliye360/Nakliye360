using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.API.CustomAttributes.RoleManagement;
using Nakliye360.Application.Abstractions.Services.LoadRequestManagement;
using Nakliye360.Application.Models.DTOs.LoadRequestManagement;
using Nakliye360.Domain.Enums;

namespace Nakliye360.API.Controllers.LoadRequestManagement
{
    /// <summary>
    /// REST controller for managing load requests.  Provides endpoints to create, list, update and delete load requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    // Require authentication for all actions; specific permissions are checked via HasPermission attributes
    [Authorize]
    public class LoadRequestController : ControllerBase
    {
        private readonly ILoadRequestService _loadRequestService;

        public LoadRequestController(ILoadRequestService loadRequestService)
        {
            _loadRequestService = loadRequestService;
        }

        /// <summary>
        /// Lists all load requests with optional filtering by load type and vehicle type.
        /// </summary>
        [HttpGet]
        [HasPermission("LoadRequest.View")]
        public async Task<IActionResult> GetAll([FromQuery] LoadType? loadType, [FromQuery] VehicleType? vehicleType)
        {
            var result = await _loadRequestService.GetAllAsync(loadType, vehicleType);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves details of a specific load request.
        /// </summary>
        [HttpGet("{id}")]
        [HasPermission("LoadRequest.View")]
        public async Task<IActionResult> Get(Guid id)
        {
            var loadRequest = await _loadRequestService.GetByIdAsync(id);
            return loadRequest == null ? NotFound() : Ok(loadRequest);
        }

        /// <summary>
        /// Creates a new load request.  Returns the identifier of the newly created entity.
        /// </summary>
        [HttpPost]
        [HasPermission("LoadRequest.Create")]
        public async Task<IActionResult> Create([FromBody] CreateLoadRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var id = await _loadRequestService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        /// <summary>
        /// Updates an existing load request.
        /// </summary>
        [HttpPut]
        [HasPermission("LoadRequest.Edit")]
        public async Task<IActionResult> Update([FromBody] UpdateLoadRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var message = await _loadRequestService.UpdateAsync(dto);
            return Ok(message);
        }

        /// <summary>
        /// Deletes an existing load request.
        /// </summary>
        [HttpDelete("{id}")]
        [HasPermission("LoadRequest.Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _loadRequestService.DeleteAsync(id);
            return NoContent();
        }
    }
}
