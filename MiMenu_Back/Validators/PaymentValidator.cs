using FluentValidation;
using MiMenu_Back.Data.DTOs;

namespace MiMenu_Back.Validators
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
    public class ItemCartValidator : AbstractValidator<CartItemGetDto>
    {
        public ItemCartValidator()
        {
            RuleFor(prop => prop.IdItem)
                .NotEmpty().WithMessage("IdItem is required")
                .Must(value => Guid.TryParse(value, out _)).WithMessage("IdItem must has format Guid");
            RuleFor(prop => prop.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be greater than or equal to 1");
            RuleFor(prop => prop.PriceUnit)
                .NotEmpty().WithMessage("PriceUnit is required")
                .GreaterThan(0).WithMessage("PriceUnit must be greater than 0");
            RuleFor(prop => prop.Food).SetValidator(new FoodDetailValidator());
        }
    }
 
    public class WebhookValidator : AbstractValidator<WebHookDto>
    {
        public WebhookValidator()
        {
            RuleFor(prop => prop.action)
                .NotEmpty().WithMessage("Action is required");
            RuleFor(prop => prop.api_version)
                .NotEmpty().WithMessage("Api_version is required");
            RuleFor(prop => prop.data.id)
                .NotEmpty().WithMessage("Data id is required");
            RuleFor(prop => prop.date_created)
                .NotEmpty().WithMessage("Date_created is required")
                .Must(value => DateTime.TryParse(value, out _)).WithMessage("Date_created must has format yyyy-MM-ddTHH:mm:ss");
            RuleFor(prop => prop.id)
                .NotEmpty().WithMessage("Id is required");
            RuleFor(prop => prop.live_mode)
                .Must(value => value == true || value == false).WithMessage("Live_mode must be boolean");
            RuleFor(prop => prop.type)
                .NotEmpty().WithMessage("Type is required")
                .Must(value => value.ToLower() == "payment").WithMessage("Type must be payment");
        }
    }
}
