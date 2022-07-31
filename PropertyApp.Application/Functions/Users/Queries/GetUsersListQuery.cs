using MediatR;

namespace PropertyApp.Application.Functions.Users.Queries;

public class GetUsersListQuery :IRequest<List<GetUsersListDto>>
{

}
