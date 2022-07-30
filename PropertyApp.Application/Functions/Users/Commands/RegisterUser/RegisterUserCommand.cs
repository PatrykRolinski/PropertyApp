using MediatR;

namespace PropertyApp.Application.Functions.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }

}
