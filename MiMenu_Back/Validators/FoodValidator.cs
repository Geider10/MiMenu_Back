using FluentValidation;
using MiMenu_Back.Data.DTOs;

namespace MiMenu_Back.Validators
{
    public class FoodAddValidator : AbstractValidator<FoodAddDto>
    {
        public FoodAddValidator()
        {
            RuleFor(p => p.IdCategory)
                .NotEmpty().WithMessage("ID category is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("ID category must has format Guid");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must has length maximum 200 characters");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(400).WithMessage("Description must has length maximum 400 characters");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(p => p.Visibility)
                .Must(value => value == true || value == false).WithMessage("Visibility must be true or false");
        }
    }
    public class FoodUpdateValidator : AbstractValidator<FoodUpdateDto>
    {
        public FoodUpdateValidator()
        {
            RuleFor(p => p.IdCategory)
                .NotEmpty().WithMessage("ID category is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("ID category must has format Guid");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must has length maximum 200 characters");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(400).WithMessage("Description must has length maximum 400 characters");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
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
