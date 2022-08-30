using FluentValidation;

namespace PropertyApp.Application.Functions.Users.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword);

            RuleFor(x => x.Password)
                .MinimumLength(4);
        }
    }
}