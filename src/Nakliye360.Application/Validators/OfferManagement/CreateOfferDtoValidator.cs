using FluentValidation;
using Nakliye360.Application.Models.DTOs.OfferManagement;

namespace Nakliye360.Application.Validators.OfferManagement
{
    /// <summary>
    /// Validates the CreateOfferDto to ensure required fields are present and valid.
    /// </summary>
    public class CreateOfferDtoValidator : AbstractValidator<CreateOfferDto>
    {
        public CreateOfferDtoValidator()
        {
            RuleFor(x => x.LoadRequestId).NotEmpty().WithMessage("Load request ID is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}