using FluentValidation;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.DTOs.Payment;
namespace MiMenu_Back.Validators.Payment
{
    public class CreatePreferenceValidator : AbstractValidator<CreatePreferenceDto>
    {
        public CreatePreferenceValidator()
        {
            RuleFor(prop => prop.IdUser)
                .NotEmpty().WithMessage("IdUser is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("IdUser must has format Guid");
            RuleFor(prop => prop.Order).SetValidator(new OrderAddValidator());
            RuleFor(prop => prop.Payment).SetValidator(new PaymentAddValidator());
            RuleForEach(prop => prop.ItemsCart).SetValidator(new ItemCartValidator());
        }
    }
    public class OrderAddValidator : AbstractValidator<OrderAddDto>
    {
        public OrderAddValidator()
        {
            RuleFor(prop => prop.Type)
                   .NotEmpty().WithMessage("Type order is required")
                   .Must(value => value.ToLower() == "takeaway" || value.ToLower() == "dinein").WithMessage("Type order must be takeaway or dinein");
            RuleFor(prop => prop.RetirementTime)
                .NotEmpty().WithMessage("RetirementTime order is required")
                .Must(value => TimeOnly.TryParse(value, out _)).WithMessage("RetirementTime must has format HH:mm");
        }
    }
    public class PaymentAddValidator : AbstractValidator<PaymentAddDto>
    {
        public PaymentAddValidator()
        {
            RuleFor(prop => prop.Currency)
                    .NotEmpty().WithMessage("Currency is required")
                    .Length(3, 4).WithMessage("Currency must has between 3 and 4 characters");
            RuleFor(prop => prop.Total)
                .GreaterThan(0).WithMessage("Total must be greater than 0");
        }
    }
}
