using FluentValidation;
using MiMenu_Back.Data.DTOs.Payment;

namespace MiMenu_Back.Validators.Payment
{
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
