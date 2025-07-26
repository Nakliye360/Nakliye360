using FluentValidation;
using Nakliye360.Application.Models.DTOs.OrderManagement;

namespace Nakliye360.Application.Validators.OrderManagement
{
    /// <summary>
    /// Validator for updating an order.
    /// Ensures identity and items are valid.
    /// </summary>
    public class UpdateOrderDtoValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
        }
    }
}
