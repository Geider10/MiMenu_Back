using FluentValidation;
using MiMenu_Back.Data.DTOs.ItemVoucher;

namespace MiMenu_Back.Validators.Voucher
{
    public class VoucherApplyValidator : AbstractValidator<VoucherApplyDto>
    {
        public VoucherApplyValidator()
        {
            RuleFor(prop => prop.TotalOrder)
                .NotEmpty().WithMessage("TotalOrder is required")
                .GreaterThan(0).WithMessage("TotalOrder must be greater than 0");
            RuleFor(prop => prop.idUser)
                .NotEmpty().WithMessage("IdUser is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("IdUser must has format Guid");
            RuleFor(prop => prop.idIV)
                .NotEmpty().WithMessage("IdItemVoucher is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("IdItemVoucher must has format Guid");
        }
    }
}
