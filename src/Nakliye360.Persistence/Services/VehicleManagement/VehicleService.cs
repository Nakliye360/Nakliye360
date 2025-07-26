using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.VehicleManagement;
using Nakliye360.Application.Models.DTOs.VehicleManagement;
using Nakliye360.Domain.Entities.VehicleManagement;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Services.VehicleManagement
{
    public class VehicleService : IVehicleService
    {
        private readonly Nakliye360DbContext _context;
        private readonly IMapper _mapper;
        public VehicleService(Nakliye360DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateVehicleAsync(CreateVehicleDto dto)
        {
            var vehicle = dto.Adapt<Vehicle>();
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task DeleteVehicleAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return;
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VehicleDto>> GetAllAsync()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            return vehicles.Adapt<List<VehicleDto>>();
        }

        public async Task<VehicleDto?> GetByIdAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            return vehicle?.Adapt<VehicleDto>();
        }

        public async Task<string> UpdateVehicleAsync(UpdateVehicleDto dto)
        {
            var vehicle = await _context.Vehicles.FindAsync(dto.Id);
            if (vehicle == null) return "Vehicle not found";
            // map fields
            _mapper.From(dto).AdaptTo(vehicle);
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return "Vehicle updated successfully.";
        }
    }
}
