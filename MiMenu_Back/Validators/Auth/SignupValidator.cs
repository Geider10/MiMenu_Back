using FluentValidation;
using MiMenu_Back.Data.DTOs.Auth;

namespace MiMenu_Back.Validators.Auth
{
    public class SignupValidator : AbstractValidator<SignupDto>
    {
        public SignupValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must has length maximum 100 characters");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").WithMessage("Email must comply format validate")
                .MaximumLength(100).WithMessage("Email must has length maximum 100 characters");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$").WithMessage("Password must comply format validate")
                .MaximumLength(100).WithMessage("Password must has length maximum 100 characters");
            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Length(10).WithMessage("Phone must has length 10 characters");
        }
    }
}
