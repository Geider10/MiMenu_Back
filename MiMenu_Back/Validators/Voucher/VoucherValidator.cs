using FluentValidation;
using MiMenu_Back.Data.DTOs.Voucher;
using System.Text.RegularExpressions;

namespace MiMenu_Back.Validators.Voucher
{
    public class VoucherUpdateValidator : AbstractValidator<VoucherUpdateDto>
    {
        public VoucherUpdateValidator()
        {
            RuleFor(prop => prop.Name)
               .NotEmpty().WithMessage("Name is required")
               .MaximumLength(100).WithMessage("Name must has length maximum 100 characters");
            RuleFor(prop => prop.Discount)
                .NotEmpty().WithMessage("Discount is required")
                .GreaterThan(0).WithMessage("Discount must be greater than 0");
            RuleFor(prop => prop.BuyMinimum)
                .NotEmpty().WithMessage("BuyMinimum is required")
                .GreaterThan(0).WithMessage("BuyMinimum must be greater than 0");
            RuleFor(prop => prop.DueDate)
                .NotEmpty().WithMessage("DueDate is required")
                .Matches(new Regex(@"^(?:3[01]|[12][0-9]|0?[1-9])([\-/.])(0?[1-9]|1[0-2])\1\d{4}$")).WithMessage("DueDate must has format validate");
        }
    }
}
