using FluentValidation;

namespace PropertyApp.Application.Functions.Properties.Commands.AddProperty;

public class CreatePropertyValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyValidator()
    {
        RuleFor(p=> p.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(p => p.Price)
            .NotEmpty();
        
        RuleFor(p=> p.Country)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.City)
            .NotEmpty()
            .MaximumLength(100); 

        RuleFor(p=> p.Street)
            .NotEmpty()
            .MaximumLength(100); 

        RuleFor(p=> p.ClosedKitchen)
            .Must(x => x == false || x == true);

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
