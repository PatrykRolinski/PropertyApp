using FluentValidation;
using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Users.Commands.RegisterUser;

public class RegisterUserValidator:AbstractValidator<RegisterUserCommand>
{
    

    public RegisterUserValidator(IUserRepository userRepository)
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Email).MustAsync(async (value, cancellation) =>
        {
            User user = await userRepository.FindyByEmail(value);
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }).WithMessage("Email must be unique");

        RuleFor(u => u.Password)
            .MinimumLength(4);

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
