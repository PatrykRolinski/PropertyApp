using FluentValidation;

namespace PropertyApp.Application.Functions.Users.Commands.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Password)
           .MinimumLength(4);


        RuleFor(u => u.ConfirmPassword)
            .Equal(u => u.Password);

        RuleFor(u => u.Token)
            .NotEmpty();
    }
}
