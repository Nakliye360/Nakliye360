using Nakliye360.Application.Models.DTOs.LoadRequestManagement;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Abstractions.Services.LoadRequestManagement
{
    /// <summary>
    /// Service contract for managing load requests posted by users.  Provides methods for CRUD and filtering operations.
    /// </summary>
    public interface ILoadRequestService
    {
        /// <summary>
        /// Lists all load requests with optional filtering by load type and vehicle type.
        /// </summary>
        /// <param name="loadType">Optional load type filter.</param>
        /// <param name="vehicleType">Optional vehicle type filter.</param>
        Task<List<LoadRequestDto>> GetAllAsync(LoadType? loadType = null, VehicleType? vehicleType = null);

        /// <summary>
        /// Retrieves a single load request by its identifier.
        /// </summary>
        Task<LoadRequestDto?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new load request and returns its identifier.
        /// </summary>
        Task<int> CreateAsync(CreateLoadRequestDto dto);

        /// <summary>
        /// Updates an existing load request.  Returns a message describing the outcome.
        /// </summary>
        Task<string> UpdateAsync(UpdateLoadRequestDto dto);

        /// <summary>
        /// Permanently deletes a load request.
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
