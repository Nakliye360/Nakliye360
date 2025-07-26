using FluentValidation;
using Nakliye360.Application.Models.DTOs.OrderManagement;

namespace Nakliye360.Application.Validators.OrderManagement
{
    /// <summary>
    /// FluentValidation validator for creating orders.
    /// Ensures that at least one item exists and quantities are positive.
    /// </summary>
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one item is required.");
            RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
