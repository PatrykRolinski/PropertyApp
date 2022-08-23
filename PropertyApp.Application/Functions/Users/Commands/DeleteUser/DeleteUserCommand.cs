using MediatR;

namespace PropertyApp.Application.Functions.Users.Commands.DeleteUser;

public class DeleteUserCommand:IRequest
{
    public Guid Id { get; set; }
}
