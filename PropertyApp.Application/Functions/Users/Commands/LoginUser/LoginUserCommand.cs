using MediatR;

namespace PropertyApp.Application.Functions.Users.Commands.LoginUser;

public class LoginUserCommand :IRequest<string>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
