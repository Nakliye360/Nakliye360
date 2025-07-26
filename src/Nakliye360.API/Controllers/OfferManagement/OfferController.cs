using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.API.CustomAttributes.RoleManagement;
using Nakliye360.Application.Abstractions.Services.OfferManagement;
using Nakliye360.Application.Models.DTOs.OfferManagement;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nakliye360.API.Controllers.OfferManagement;

/// <summary>
/// REST controller for managing offers associated with load requests.  Updated to return IActionResult
/// and to use the new UpdateAsync method that performs assignment logic when an offer is accepted.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    /// <summary>
    /// Gets all offers for a specific load request.  Carriers and customers can view offers.
    /// </summary>
    [HttpGet("by-load/{loadRequestId}")]
    [HasPermission("Offer.View")]
    public async Task<IActionResult> GetByLoadRequest(Guid loadRequestId)
    {
        var offers = await _offerService.GetByLoadRequestAsync(loadRequestId);
        return Ok(offers);
    }

    /// <summary>
    /// Gets details of a single offer by its id.
    /// </summary>
    [HttpGet("{id}")]
    [HasPermission("Offer.View")]
    public async Task<IActionResult> Get(Guid id)
    {
        var offer = await _offerService.GetByIdAsync(id);
        return offer == null ? NotFound() : Ok(offer);
    }

    /// <summary>
    /// Creates a new offer for a load request.  Only carriers are allowed to create offers.
    /// </summary>
    [HttpPost]
    [HasPermission("Offer.Create")]
    public async Task<IActionResult> Create([FromBody] CreateOfferDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Retrieve the current user's identifier (assumed to be a Guid) from claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
            return Unauthorized();

        var carrierId = Guid.Parse(userIdClaim);
        var id = await _offerService.CreateAsync(dto, carrierId);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    /// <summary>
    /// Updates an existing offer.  Carriers may adjust their price or comments; customers may change the status to accept or reject.
    /// When an offer is accepted, other offers are automatically rejected and the load request status is updated.
    /// </summary>
    [HttpPut]
    [HasPermission("Offer.Edit")]
    public async Task<IActionResult> Update([FromBody] UpdateOfferDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var message = await _offerService.UpdateAsync(dto);
        return Ok(message);
    }

    /// <summary>
    /// Deletes an offer.  Carriers can withdraw their offer; operators/admin may remove inappropriate offers.
    /// </summary>
    [HttpDelete("{id}")]
    [HasPermission("Offer.Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _offerService.DeleteAsync(id);
        return NoContent();
    }
}