using FluentValidation;
using MiMenu_Back.Data.DTOs.User;

namespace MiMenu_Back.Validators.User
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(u => u.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required and length maximum is 100 characters");
            RuleFor(u => u.Address).NotEmpty().MaximumLength(200).WithMessage("Address length maximum is 200 characters");

        }
    }
}
