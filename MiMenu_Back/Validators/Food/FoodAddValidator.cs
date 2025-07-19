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
                .MaximumLength(50).WithMessage("ID category must has length maximum 50 characters");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must has length maximum 200 characters");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(400).WithMessage("Description must has length 400 characters");
            RuleFor(p => p.ImgUrl)
                .NotEmpty().WithMessage("ImgUrl is required")
                .MaximumLength(400).WithMessage("ImgUrl must has length maximum 400 characters");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
