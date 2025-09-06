using FluentValidation;
using MiMenu_Back.Data.DTOs;

namespace MiMenu_Back.Validators
{
    public class BannerAddValidator : AbstractValidator<BannerAddDto>
    {
        public BannerAddValidator()
        {
            RuleFor(prop => prop.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(250).WithMessage("Description must has length maximum 250 characters");
            RuleFor(prop => prop.Priority)
                .NotEmpty().WithMessage("Priority is required")
                .GreaterThan(0).WithMessage("Priority must be greater than 0");
            RuleFor(prop => prop.visibility)
                .Must(value => value == true || value == false).WithMessage("Visibility must be true or false");
        }
    }
}
