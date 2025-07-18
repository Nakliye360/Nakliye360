using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.Application.Abstractions.Services.CustomerManagement;
using Nakliye360.Application.Abstractions.Session;
using Nakliye360.Application.Models.DTOs.CustomerManagement;

namespace Nakliye360.API.Controllers.CustomerManagement;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomerController : ControllerBase
{

    private readonly ICustomerService _customerService;
    private readonly ICurrentUserSession _currentUserSession;

    public CustomerController(ICustomerService customerService, ICurrentUserSession currentUserSession)
    {
        _customerService = customerService;
        _currentUserSession = currentUserSession;
    }

    /// <summary>
    /// Yeni müşteri oluşturur.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        dto.AppUserId = _currentUserSession.UserId;
        var id = await _customerService.CreateCustomerAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>
    /// Mevcut müşteri günceller.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
    {
        if (dto.Id != id)
            return BadRequest("Id uyuşmuyor.");

        var result = await _customerService.UpdateCustomerAsync(dto);
        return result ? NoContent() : NotFound();
    }

    /// <summary>
    /// Müşteriyi ID ile getirir.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        return Ok(customer);
    }

    /// <summary>
    /// Müşteri listesini filtreler.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetList([FromQuery] CustomerListFilterDto filter)
    {
        var list = await _customerService.GetCustomerListAsync(filter);
        return Ok(list);
    }

    /// <summary>
    /// Müşteriyi siler.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        return result ? NoContent() : NotFound();
    }
}
