using FluentValidation;
using PropertyApp.Application.Contracts;

namespace PropertyApp.Application.Functions.Users.Commands.UpdateUser
{
    public class UpdateUserValidator:AbstractValidator<UpdateUserCommand>
    {
       public UpdateUserValidator(IUserRepository userRepository)
        {
            RuleFor(u => u.UserId).MustAsync(async (value, cancellation) =>
            {
                var user = await userRepository.GetByIdAsync(value);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage($"User not Found");

            RuleFor(u => u.FirstName)
            .NotEmpty()
            .MaximumLength(50);

            RuleFor(u => u.LastName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}