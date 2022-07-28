using FluentValidation;

namespace PropertyApp.Application.Functions.Properties.Commands.UpdateProperty;

public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyValidator()
    {
        RuleFor(p => p.Description)
           .NotEmpty()
           .MaximumLength(2000);

        RuleFor(p => p.Price)
            .NotEmpty();

        RuleFor(p => p.Country)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.City)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.Street)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.ClosedKitchen)
            .NotEmpty();

        // TODO: FIX Enum Validation
        RuleFor(p => p.MarketType)
            .IsInEnum();

        RuleFor(p => p.PropertyStatus)
            .IsInEnum();

        RuleFor(p => p.PropertyType)
            .IsInEnum()
            .WithMessage("Not in Enum");
    }
}
