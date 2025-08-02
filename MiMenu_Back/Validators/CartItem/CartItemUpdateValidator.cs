using FluentValidation;
using MiMenu_Back.Data.DTOs.Order;
namespace MiMenu_Back.Validators.Order
{
    public class CartItemUpdateValidator : AbstractValidator<CartItemUpdateDto>
    {
        public CartItemUpdateValidator()
        {
            RuleFor(prop => prop.Quantity)
               .NotEmpty().WithMessage("Quantity is required")
               .GreaterThan(0).WithMessage("Quantity must be greater than 0");
            RuleFor(prop => prop.PriceTotal)
                .NotEmpty().WithMessage("PriceTotal is required")
                .GreaterThan(0).WithMessage("PriceTotal must be greater than 0");
        }
    }
}
