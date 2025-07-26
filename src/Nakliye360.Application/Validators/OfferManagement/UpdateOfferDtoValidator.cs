using FluentValidation;
using Nakliye360.Application.Models.DTOs.OfferManagement;

namespace Nakliye360.Application.Validators.OfferManagement
{
    /// <summary>
    /// Validates the UpdateOfferDto to ensure the identifier exists and fields are valid.
    /// </summary>
    public class UpdateOfferDtoValidator : AbstractValidator<UpdateOfferDto>
    {
        public UpdateOfferDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Offer ID is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}