using MediatR;

namespace PropertyApp.Application.Functions.Users.Queries.GetUsersList;

public class GetUsersListQuery :IRequest<List<GetUsersListDto>>
{

}
