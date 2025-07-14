using FluentValidation;
using MiMenu_Back.Data.DTOs.Auth;

namespace MiMenu_Back.Validators.Auth
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Email).NotEmpty().MaximumLength(100).Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").WithMessage("Email  must has format validate");
            RuleFor(u => u.Password).NotEmpty().MaximumLength(100).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$").WithMessage("Password must comply format expected");
        }
    }
}
