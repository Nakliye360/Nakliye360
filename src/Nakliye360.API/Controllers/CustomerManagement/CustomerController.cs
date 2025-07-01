using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.API.CustomAttributes.RoleManagement;
using Nakliye360.API.Filters;
using Nakliye360.Application.Abstractions.Services.CustomerManagement;
using Nakliye360.Application.Models.DTOs.CustomerManagement;
using System.ComponentModel.DataAnnotations;

namespace Nakliye360.API.Controllers.CustomerManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin,Manager")]
        [HasPermission("Customer.Updated")]
        [ServiceFilter(typeof(ValidationFilter<CreateCustomerDto>))]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {

            var customerId = await _customerService.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = customerId }, null);

        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Manager")]
        [HasPermission("Customer.Updated")]
        [ServiceFilter(typeof(ValidationFilter<UpdateCustomerDto>))]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCustomerDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ve body ID'leri eşleşmiyor.");

            var result = await _customerService.UpdateCustomerAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }

    }
}
