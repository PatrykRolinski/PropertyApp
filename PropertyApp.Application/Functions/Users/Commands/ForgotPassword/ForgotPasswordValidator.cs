using FluentValidation;

namespace PropertyApp.Application.Functions.Users.Commands.ForgotPassword
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x=> x.Email).NotEmpty();
        }
    }
}