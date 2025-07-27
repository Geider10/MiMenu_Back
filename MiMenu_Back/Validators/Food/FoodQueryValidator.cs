using FluentValidation;
using MiMenu_Back.Data.DTOs.Food;

namespace MiMenu_Back.Validators.Food
{
    public class FoodQueryValidator : AbstractValidator<FoodQueryDto>
    {
        public FoodQueryValidator()
        {
            RuleFor(f => f.IdCategory)
                .Must( v => Guid.TryParse(v, out _) || string.IsNullOrEmpty(v)).WithMessage("IdCategory must has format Guid");
            RuleFor(f => f.Sort)
                .Must(v => v == "asc" || v == "desc" || string.IsNullOrEmpty(v)).WithMessage("Sort must be asc or desc");
        }
    }
}
