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
            RuleFor(prop => prop.PriceUnit)
                .NotEmpty().WithMessage("PriceUnit is required")
                .GreaterThan(0).WithMessage("PriceUnit must be greater than 0");
        }
    }
}
