using MediatR;
using PropertyApp.Domain.Enums;

namespace PropertyApp.Application.Functions.Users.Commands.ChangeRole
{
    public class ChangeRoleCommand:IRequest
    {
        public RoleName Role { get; set; }
        public Guid UserId { get; set; }
    }
}