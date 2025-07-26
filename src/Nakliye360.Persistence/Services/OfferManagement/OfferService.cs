using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.OfferManagement;
using Nakliye360.Application.Models.DTOs.OfferManagement;
using Nakliye360.Domain.Entities.OfferManagement;
using Nakliye360.Domain.Enums;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Services.OfferManagement
{
    /// <summary>
    /// Persistence layer implementation for managing offers.  Uses Mapster to map between entity and DTO objects.
    /// </summary>
    public class OfferService : IOfferService
    {
        private readonly Nakliye360DbContext _context;
        private readonly IMapper _mapper;

        public OfferService(Nakliye360DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OfferDto>> GetByLoadRequestAsync(Guid loadRequestId)
        {
            var offers = await _context.Set<Offer>()
                .Where(o => o.LoadRequestId == loadRequestId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<OfferDto>>(offers);
        }

        public async Task<OfferDto?> GetByIdAsync(Guid id)
        {
            var offer = await _context.Set<Offer>().FindAsync(id);
            return offer == null ? null : _mapper.Map<OfferDto>(offer);
        }

        public async Task<Guid> CreateAsync(CreateOfferDto dto, Guid carrierId)
        {
            var entity = _mapper.Map<Offer>(dto);
            entity.Id = Guid.NewGuid();
            entity.CarrierId = carrierId;
            entity.OfferedDate = DateTime.UtcNow;
            entity.Status = OfferStatus.Pending;
            _context.Set<Offer>().Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<string> UpdateAsync(UpdateOfferDto dto)
        {
            var entity = await _context.Set<Offer>().FindAsync(dto.Id);
            if (entity == null)
                throw new InvalidOperationException("Offer not found");

            entity.Price = dto.Price;
            entity.Comment = dto.Comment;
            entity.Status = dto.Status;
            await _context.SaveChangesAsync();
            return "Offer updated successfully";
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<Offer>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<Offer>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}