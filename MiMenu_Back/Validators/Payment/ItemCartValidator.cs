using FluentValidation;
using MiMenu_Back.Data.DTOs.CartItem;
using MiMenu_Back.Data.DTOs.Order;

namespace MiMenu_Back.Validators.Payment
{
    public class ItemCartValidator : AbstractValidator<CartItemGetDto>
    {
        public ItemCartValidator()
        {
            RuleFor(prop => prop.IdItem)
                .NotEmpty().WithMessage("IdItem is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("IdItem must has format Guid");
            RuleFor(prop => prop.Quantity)
                .NotEmpty().WithMessage("Quantity is required") 
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be greater than or equal to 1");
            RuleFor(prop => prop.PriceUnit)
                .NotEmpty().WithMessage("PriceUnit is required")
                .GreaterThan(0).WithMessage("PriceUnit must be greater than 0");
            RuleFor(prop => prop.Food).SetValidator(new FoodDetailValidator());
        }
    }
    public class FoodDetailValidator : AbstractValidator<FoodDetailDto>
    {
        public FoodDetailValidator()
        {
            RuleFor(prop => prop.IdFood)
                .NotEmpty().WithMessage("IdFood is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("IdFood must has format Guid");
            RuleFor(prop => prop.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must has length maximum 200 characters");
            RuleFor(prop => prop.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(400).WithMessage("Description must has length maximum 400 characters");
            When(prop => prop.Discount.HasValue, () =>
            {
                RuleFor(prop => prop)
                    .Must(prop => prop.Discount >= 0 && prop.Discount <= 100).WithMessage("Discount must be between 0 and 100");
            });
        }
    }
}
