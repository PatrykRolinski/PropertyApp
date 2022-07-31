using MediatR;

namespace PropertyApp.Application.Functions.Users.Commands.VerifyUser;

public class VerifyUserCommand:IRequest
{
    public string? Token { get; set; }
}
