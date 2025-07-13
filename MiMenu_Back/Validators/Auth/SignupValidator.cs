using FluentValidation;
using MiMenu_Back.Data.DTOs.Auth;

namespace MiMenu_Back.Validators.Auth
{
    public class SignupValidator : AbstractValidator<SignupDto>
    {
        public SignupValidator()
        {
            RuleFor(u => u.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required and length maximum is 100 characters");
            RuleFor(u => u.Email).NotEmpty().MaximumLength(100).Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").WithMessage("Email is required and must has format validate");
            RuleFor(u => u.Password).NotEmpty().MaximumLength(100).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$").WithMessage("Password is required and must comply format expected");
            RuleFor(u => u.Address).NotEmpty().MaximumLength(200).WithMessage("Address length maximum is 200 characters");
            //RuleFor(u => u.BirthDate).Matches(@"^(?: 3[01] | [12][0 - 9] | 0?[1 - 9])([\-/.])(0?[1 - 9] | 1[1 - 2])\1\d{ 4}$").WithMessage("Birth date is optional and must comply format expected");
        }
    }
}
