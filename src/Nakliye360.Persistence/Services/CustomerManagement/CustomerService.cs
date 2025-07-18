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
        var customer = _mapper.Map<Customer>(dto);

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync(); // ID üretimi

        // Alt türü ekle
        await AddCustomerDetailsAsync(customer.Id, dto);

        await _context.SaveChangesAsync();
        return customer.Id;
    }

    private async Task AddCustomerDetailsAsync(int customerId, CreateCustomerDto dto)
    {
        switch (dto.CustomerType)
        {
            case CustomerType.Individual:
                if (dto.FirstName == null || dto.LastName == null)
                    throw new ArgumentException("IndividualCustomer için ad ve soyad zorunludur.");

                await _context.IndividualCustomers.AddAsync(new IndividualCustomer
                {
                    CustomerId = customerId,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                });
                break;

            case CustomerType.Corporate:
                if (dto.CompanyName == null || dto.TaxNumber == null)
                    throw new ArgumentException("CorporateCustomer için firma adı ve vergi no zorunludur.");

                await _context.CorporateCustomers.AddAsync(new CorporateCustomer
                {
                    CustomerId = customerId,
                    CompanyName = dto.CompanyName,
                    TaxNumber = dto.TaxNumber
                });
                break;

            //case CustomerType.Producer:
            //    if (dto.ProductionType == null)
            //        throw new ArgumentException("ProducerCustomer için üretim türü zorunludur.");

            //    await _context.ProducerCustomers.AddAsync(new ProducerCustomer
            //    {
            //        CustomerId = customerId,
            //        CompanyName = dto.CompanyName, // opsiyonel
            //        ProductionType = dto.ProductionType
            //    });
            //    break;

            default:
                throw new ArgumentOutOfRangeException(nameof(dto.CustomerType), "Geçersiz müşteri türü.");
        }
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            throw new KeyNotFoundException("Müşteri bulunamadı.");

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    
  
    public async Task<CustomerDto> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customers
            .Include(c => c.IndividualCustomer)
            .Include(c => c.CorporateCustomer)
            .Include(c => c.ProducerCustomer)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
            throw new KeyNotFoundException("Müşteri bulunamadı.");

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<bool> UpdateCustomerAsync(UpdateCustomerDto dto)
    {
        var customer = await _context.Customers
            .Include(c => c.IndividualCustomer)
            .Include(c => c.CorporateCustomer)
            .Include(c => c.ProducerCustomer)
            .FirstOrDefaultAsync(c => c.Id == dto.Id);

        if (customer == null)
            throw new KeyNotFoundException("Müşteri bulunamadı.");

        customer.PhoneNumber = dto.PhoneNumber;
        customer.Email = dto.Email;
        customer.Address = dto.Address;

        switch (customer.CustomerType)
        {
            case CustomerType.Individual:
                if (customer.IndividualCustomer != null)
                {
                    customer.IndividualCustomer.FirstName = dto.FirstName!;
                    customer.IndividualCustomer.LastName = dto.LastName!;
                }
                break;

            case CustomerType.Corporate:
                if (customer.CorporateCustomer != null)
                {
                    customer.CorporateCustomer.CompanyName = dto.CompanyName!;
                    customer.CorporateCustomer.TaxNumber = dto.TaxNumber!;
                }
                break;

            case CustomerType.Producer:
                if (customer.ProducerCustomer != null)
                {
                    customer.ProducerCustomer.ProductionType = dto.ProductionType!;
                    customer.ProducerCustomer.CompanyName = dto.CompanyName;
                }
                break;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CustomerDto>> GetCustomerListAsync(CustomerListFilterDto filter)
    {
        var query = _context.Customers
        .Include(c => c.IndividualCustomer)
        .Include(c => c.CorporateCustomer)
        .Include(c => c.ProducerCustomer)
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(c =>
                c.IdentityNumber.Contains(filter.Search) ||
                c.PhoneNumber.Contains(filter.Search) ||
                c.Email.Contains(filter.Search));
        }

        if (filter.CustomerType.HasValue)
        {
            query = query.Where(c => c.CustomerType == filter.CustomerType.Value);
        }

        var list = await query.OrderByDescending(c => c.Id).ToListAsync();
        return _mapper.Map<List<CustomerDto>>(list);
    }

    public Task<bool> ExistsByIdentityNumberAsync(string identityNumber)
    {
        return _context.Customers.AnyAsync(x => x.IdentityNumber == identityNumber); // TC/VKN kontrolü
    }

}
