using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.OfferManagement;
using Nakliye360.Application.Models.DTOs.OfferManagement;
using Nakliye360.Domain.Entities.OfferManagement;
using Nakliye360.Domain.Entities.LoadRequestManagement;
using Nakliye360.Domain.Enums;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Services.OfferManagement;

/// <summary>
/// Persistence layer implementation for managing offers.  Uses Mapster to map between entity and DTO objects.
/// Updated to support acceptance logic: when an offer is accepted all other offers are automatically
/// rejected and the corresponding load request is marked as assigned.
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

    /// <summary>
    /// Updates an existing offer.  If the offer status is changed to Accepted, all other offers for the
    /// same load request are rejected and the load request is marked as Assigned.
    /// </summary>
    public async Task<string> UpdateAsync(UpdateOfferDto dto)
    {
        var entity = await _context.Set<Offer>().FindAsync(dto.Id);
        if (entity == null)
            throw new InvalidOperationException("Offer not found");

        // update price and comment
        entity.Price = dto.Price;
        entity.Comment = dto.Comment;

        var previousStatus = entity.Status;
        entity.Status = dto.Status;

        // When an offer is accepted, perform assignment logic
        if (previousStatus != OfferStatus.Accepted && dto.Status == OfferStatus.Accepted)
        {
            // Reject all other offers for the same load request
            var otherOffers = await _context.Set<Offer>()
                .Where(o => o.LoadRequestId == entity.LoadRequestId && o.Id != entity.Id)
                .ToListAsync();
            foreach (var other in otherOffers)
            {
                other.Status = OfferStatus.Rejected;
            }

            // Mark the underlying load request as assigned
            var loadRequest = await _context.Set<LoadRequest>().FindAsync(entity.LoadRequestId);
            if (loadRequest != null)
            {
                loadRequest.Status = LoadRequestStatus.Assigned;
            }
        }

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