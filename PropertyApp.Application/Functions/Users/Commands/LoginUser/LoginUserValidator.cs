using FluentValidation;


namespace PropertyApp.Application.Functions.Users.Commands.LoginUser;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x=> x.Password).NotEmpty();
        RuleFor(x=> x.Email).NotEmpty();
    }
}
