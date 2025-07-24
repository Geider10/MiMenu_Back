using FluentValidation;
using MiMenu_Back.Data.DTOs.Order;

namespace MiMenu_Back.Validators.Order
{
    public class OrderAddValidator : AbstractValidator<OrderAddDto>
    {
        public OrderAddValidator()
        {
            RuleFor(prop => prop.IdFood)
                .NotEmpty().WithMessage("IdFood is required")
                .Must(v => Guid.TryParse(v, out _)).WithMessage("IdFood must has format Guid");
            RuleFor(prop => prop.IdUser)
                .NotEmpty().WithMessage("IdUser is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("IdUser must has format Guid");
            RuleFor(prop => prop.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
            RuleFor(prop => prop.PriceTotal)
                .NotEmpty().WithMessage("PriceTotal is required")
                .GreaterThan(0).WithMessage("PriceTotal must be greater than 0");
        }
    }
}
