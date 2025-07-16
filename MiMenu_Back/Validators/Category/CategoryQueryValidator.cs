using FluentValidation;
using MiMenu_Back.Data.DTOs.Category;

namespace MiMenu_Back.Validators.Category
{
    public class CategoryQueryValidator : AbstractValidator<CategoryQueryDto>
    {
        public CategoryQueryValidator()
        {
            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("Type is required")
                .Must(t => t.ToLower() == "comida" || t.ToLower() == "cupón").WithMessage("Type must be comida or cupón");
            RuleFor(c => c.Sort)
                .Must(s => s == "asc" || s == "desc" || string.IsNullOrEmpty(s)).WithMessage("Sort must be asc, desc, or empty");
        }
    }
}
