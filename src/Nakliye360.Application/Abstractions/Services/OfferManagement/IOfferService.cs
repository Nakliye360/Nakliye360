using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakliye360.Application.Models.DTOs.OfferManagement;

namespace Nakliye360.Application.Abstractions.Services.OfferManagement
{
    /// <summary>
    /// Contract for application layer services responsible for managing offers.
    /// </summary>
    public interface IOfferService
    {
        /// <summary>
        /// Returns all offers for a given load request.
        /// </summary>
        Task<IEnumerable<OfferDto>> GetByLoadRequestAsync(Guid loadRequestId);

        /// <summary>
        /// Retrieves a single offer by its identifier.
        /// </summary>
        Task<OfferDto?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new offer made by a carrier.  Returns the id of the created offer.
        /// </summary>
        Task<Guid> CreateAsync(CreateOfferDto dto, Guid carrierId);

        /// <summary>
        /// Updates an existing offer (e.g. price or status).
        /// </summary>
        Task<string> UpdateAsync(UpdateOfferDto dto);

        /// <summary>
        /// Removes an offer from the system.
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}