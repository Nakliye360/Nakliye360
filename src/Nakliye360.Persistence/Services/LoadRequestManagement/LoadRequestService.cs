using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.LoadRequestManagement;
using Nakliye360.Application.Models.DTOs.LoadRequestManagement;
using Nakliye360.Domain.Entities.LoadRequestManagement;
using Nakliye360.Domain.Enums;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Services.LoadRequestManagement
{
    /// <summary>
    /// Service responsible for persisting and retrieving load requests.  Implements filtering logic based on load and vehicle types.
    /// </summary>
    public class LoadRequestService : ILoadRequestService
    {
        private readonly Nakliye360DbContext _context;
        private readonly IMapper _mapper;

        public LoadRequestService(Nakliye360DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateLoadRequestDto dto)
        {
            var entity = dto.Adapt<LoadRequest>();
            await _context.LoadRequests.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.LoadRequests.FindAsync(id);
            if (entity == null) return;
            _context.LoadRequests.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LoadRequestDto>> GetAllAsync(LoadType? loadType = null, VehicleType? vehicleType = null)
        {
            var query = _context.LoadRequests.AsQueryable();
            if (loadType.HasValue)
            {
                query = query.Where(l => l.LoadType == loadType);
            }
            if (vehicleType.HasValue)
            {
                query = query.Where(l => l.VehicleType == vehicleType);
            }
            var list = await query.ToListAsync();
            return list.Adapt<List<LoadRequestDto>>();
        }

        public async Task<LoadRequestDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.LoadRequests.FindAsync(id);
            return entity?.Adapt<LoadRequestDto>();
        }

        public async Task<string> UpdateAsync(UpdateLoadRequestDto dto)
        {
            var entity = await _context.LoadRequests.FindAsync(dto.Id);
            if (entity == null)
            {
                return "Load request not found";
            }
            // Apply changes from DTO onto the existing entity
            _mapper.From(dto).AdaptTo(entity);
            _context.LoadRequests.Update(entity);
            await _context.SaveChangesAsync();
            return "Load request updated successfully.";
        }
    }
}
