using FluentValidation;

namespace PropertyApp.Application.Functions.Users.Commands.RegisterUser;

public class RegisterUserValidator:AbstractValidator<RegisterUserCommand>
{
   public RegisterUserValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(u => u.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(u=> u.ConfirmPassword)
            .Equal(u=> u.Password);
    }
}
