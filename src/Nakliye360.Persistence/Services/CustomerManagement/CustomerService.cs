using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.CustomerManagement;
using Nakliye360.Application.Models.DTOs.CustomerManagement;
using Nakliye360.Domain.Entities.CustomerManagement;
using Nakliye360.Domain.Enums;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Services.CustomerManagement;

public class CustomerService : ICustomerService
{
    private readonly Nakliye360DbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(Nakliye360DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> CreateCustomerAsync(CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            AppUserId = dto.AppUserId,
            CustomerType = dto.CustomerType,
            IdentityNumber = dto.IdentityNumber,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Address = dto.Address,
        };

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync(); // Customer.Id için

        switch (dto.CustomerType)
        {
            case CustomerType.Individual:
                await _context.IndividualCustomers.AddAsync(new IndividualCustomer
                {
                    CustomerId = customer.Id,
                    FirstName = dto.FirstName!,
                    LastName = dto.LastName!
                });
                break;

            case CustomerType.Corporate:
                await _context.CorporateCustomers.AddAsync(new CorporateCustomer
                {
                    CustomerId = customer.Id,
                    CompanyName = dto.CompanyName!,
                    TaxNumber = dto.TaxNumber!
                });
                break;

            case CustomerType.Producer:
                await _context.ProducerCustomers.AddAsync(new ProducerCustomer
                {
                    CustomerId = customer.Id,
                    CompanyName = dto.CompanyName, // opsiyonel
                    ProductionType = dto.ProductionType!
                });
                break;
        }

        await _context.SaveChangesAsync();
        return customer.Id;
    }

    public async Task DeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
            throw new Exception("Customer not found.");

        switch (customer.CustomerType)
        {
            case CustomerType.Individual:
                var individual = await _context.IndividualCustomers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                if (individual != null) _context.IndividualCustomers.Remove(individual);
                break;
            case CustomerType.Corporate:
                var corporate = await _context.CorporateCustomers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                if (corporate != null) _context.CorporateCustomers.Remove(corporate);
                break;
            case CustomerType.Producer:
                var producer = await _context.ProducerCustomers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                if (producer != null) _context.ProducerCustomers.Remove(producer);
                break;
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsByIdentityNumberAsync(string identityNumber)
    {
        return _context.Customers.AnyAsync(x => x.IdentityNumber == identityNumber); // TC/VKN kontrolü
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var customers = await _context.Customers.Where(c=> !c.IsDeleted).ToListAsync();
        var customerDtos = new List<CustomerDto>();

        foreach (var customer in customers)
        {
            var dto = customer.Adapt<CustomerDto>();

            switch (customer.CustomerType)
            {
                case CustomerType.Individual:
                    var individual = await _context.IndividualCustomers.FirstOrDefaultAsync(x => x.CustomerId == customer.Id);
                    dto.FullName = $"{individual?.FirstName} {individual?.LastName}";
                    break;

                case CustomerType.Corporate:
                    var corporate = await _context.CorporateCustomers.FirstOrDefaultAsync(x => x.CustomerId == customer.Id);
                    dto.CompanyName = corporate?.CompanyName;
                    dto.TaxNumber = corporate?.TaxNumber;
                    break;

                case CustomerType.Producer:
                    var producer = await _context.ProducerCustomers.FirstOrDefaultAsync(x => x.CustomerId == customer.Id);
                    dto.CompanyName = producer?.CompanyName;
                    dto.ProductionType = producer?.ProductionType;
                    break;
            }

            customerDtos.Add(dto);
        }

        return customerDtos;
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(int customerId)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
        if (customer == null) return null;

        var dto = customer.Adapt<CustomerDto>();

        switch (customer.CustomerType)
        {
            case CustomerType.Individual:
                var individual = await _context.IndividualCustomers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                dto.FullName = $"{individual?.FirstName} {individual?.LastName}";
                break;
            case CustomerType.Corporate:
                var corporate = await _context.CorporateCustomers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                dto.CompanyName = corporate?.CompanyName;
                dto.TaxNumber = corporate?.TaxNumber;
                break;
            case CustomerType.Producer:
                var producer = await _context.ProducerCustomers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
                dto.CompanyName = producer?.CompanyName;
                dto.ProductionType = producer?.ProductionType;
                break;
        }

        return dto;
    }

    public async Task<string> UpdateCustomerAsync(UpdateCustomerDto request)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (customer is null)
            throw new Exception("Customer not found");

        _mapper.From(request).AdaptTo(customer);
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();

        return "Customer updated successfully.";
    }
}
