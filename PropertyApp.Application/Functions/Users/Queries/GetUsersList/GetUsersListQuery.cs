using MediatR;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Users.Queries.GetUsersList;

public class GetUsersListQuery :IRequest<PageResult<GetUsersListDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageSize { get; set; } = 50;
    public int PageNumber { get; set; }
}
