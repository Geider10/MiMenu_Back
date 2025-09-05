using FluentValidation;
using MiMenu_Back.Data.DTOs.Category;

namespace MiMenu_Back.Validators
{
    public class CategoryAddValidator : AbstractValidator<CategoryAddDto>
    {
        public CategoryAddValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must has length maximum 100 characters");
            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("Type is required")
                .MaximumLength(50).WithMessage("Type must has length maximum 50 characters");
            RuleFor(p => p.Visibility)
                .Must(value => value == true || value == false).WithMessage("Visibility must be true or false");
        }
    }
}
