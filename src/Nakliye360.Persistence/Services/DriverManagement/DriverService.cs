using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.DriverManagement;
using Nakliye360.Application.Models.DTOs.DriverManagement;
using Nakliye360.Domain.Entities.DriverManagement;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Services.DriverManagement
{
    public class DriverService : IDriverService
    {
        private readonly Nakliye360DbContext _context;
        private readonly IMapper _mapper;
        public DriverService(Nakliye360DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateDriverAsync(CreateDriverDto dto)
        {
            var driver = dto.Adapt<Driver>();
            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
            return driver.Id;
        }

        public async Task DeleteDriverAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return;
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DriverDto>> GetAllAsync()
        {
            var drivers = await _context.Drivers.ToListAsync();
            return drivers.Adapt<List<DriverDto>>();
        }

        public async Task<DriverDto?> GetByIdAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            return driver?.Adapt<DriverDto>();
        }

        public async Task<string> UpdateDriverAsync(UpdateDriverDto dto)
        {
            var driver = await _context.Drivers.FindAsync(dto.Id);
            if (driver == null) return "Driver not found";
            _mapper.From(dto).AdaptTo(driver);
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return "Driver updated successfully.";
        }
    }
}
