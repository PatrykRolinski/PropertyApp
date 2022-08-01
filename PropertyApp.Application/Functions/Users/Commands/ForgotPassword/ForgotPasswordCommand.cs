using MediatR;

namespace PropertyApp.Application.Functions.Users.Commands.ForgotPassword;

public class ForgotPasswordCommand:IRequest
{
    public string? Email { get; set; }
}
