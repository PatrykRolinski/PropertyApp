using MediatR;

namespace PropertyApp.Application.Functions.Users.Commands.ChangePassword;

public class ChangePasswordCommand:IRequest
{
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }

}
