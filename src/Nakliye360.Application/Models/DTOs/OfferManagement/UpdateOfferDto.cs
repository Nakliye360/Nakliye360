using System;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.OfferManagement
{
    /// <summary>
    /// Model used to update an existing offer.  It can be used by carriers to adjust price or by customers to change status.
    /// </summary>
    public class UpdateOfferDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string? Comment { get; set; }
        public OfferStatus Status { get; set; }
    }
}