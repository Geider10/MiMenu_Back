using FluentValidation;
using MiMenu_Back.Data.DTOs.Category;

namespace MiMenu_Back.Validators.Category
{
    public class AttributeValidator : AbstractValidator<AttributeDto>
    {
        public AttributeValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must has length maximum is 100 characters");
        }
    }
}
