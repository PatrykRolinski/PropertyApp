using MediatR;

namespace PropertyApp.Application.Functions.Users.Commands.UpdateUser;

public class UpdateUserCommand: IRequest
{
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
