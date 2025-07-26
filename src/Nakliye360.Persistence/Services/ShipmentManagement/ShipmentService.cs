using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.ShipmentManagement;
using Nakliye360.Application.Models.DTOs.ShipmentManagement;
using Nakliye360.Domain.Entities.ShipmentManagement;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Services.ShipmentManagement
{
    /// <summary>
    /// Service implementation for managing shipments in the database.
    /// </summary>
    public class ShipmentService : IShipmentService
    {
        private readonly Nakliye360DbContext _context;
        private readonly IMapper _mapper;

        public ShipmentService(Nakliye360DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateShipmentAsync(CreateShipmentDto dto)
        {
            var shipment = dto.Adapt<Shipment>();
            await _context.Set<Shipment>().AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment.Id;
        }

        public async Task DeleteShipmentAsync(int id)
        {
            var shipment = await _context.Set<Shipment>().FindAsync(id);
            if (shipment == null) return;
            _context.Set<Shipment>().Remove(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShipmentDto>> GetAllAsync()
        {
            var shipments = await _context.Set<Shipment>().ToListAsync();
            return shipments.Adapt<List<ShipmentDto>>();
        }

        public async Task<ShipmentDto?> GetByIdAsync(int id)
        {
            var shipment = await _context.Set<Shipment>().FindAsync(id);
            return shipment?.Adapt<ShipmentDto>();
        }

        public async Task<string> UpdateShipmentAsync(UpdateShipmentDto dto)
        {
            var shipment = await _context.Set<Shipment>().FindAsync(dto.Id);
            if (shipment == null) return "Shipment not found";
            _mapper.From(dto).AdaptTo(shipment);
            _context.Set<Shipment>().Update(shipment);
            await _context.SaveChangesAsync();
            return "Shipment updated successfully.";
        }
    }
}