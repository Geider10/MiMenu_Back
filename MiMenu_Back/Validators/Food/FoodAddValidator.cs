using FluentValidation;
using MiMenu_Back.Data.DTOs.Food;

namespace MiMenu_Back.Validators.Food
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
}
