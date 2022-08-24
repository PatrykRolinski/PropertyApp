using MediatR;

namespace PropertyApp.Application.Functions.Users.Queries.GetUser;

public class GetUserQuery:IRequest<GetUserDto>
{
    public Guid Id { get; set; }
}
