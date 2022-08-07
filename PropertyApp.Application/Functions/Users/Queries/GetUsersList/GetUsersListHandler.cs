using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;

namespace PropertyApp.Application.Functions.Users.Queries.GetUsersList;

public class GetUsersListHandler : IRequestHandler<GetUsersListQuery, List<GetUsersListDto>>
{
    private readonly IUserRepository _userRespository;
    private readonly IMapper _mapper;

    public GetUsersListHandler(IUserRepository userRespository, IMapper mapper)
    {
        _userRespository = userRespository;
        _mapper = mapper;
    }

    public async Task<List<GetUsersListDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {
      var users=await _userRespository.GetAllAsync();
      var usersDto=_mapper.Map<List<GetUsersListDto>>(users);
       return usersDto;
    }
}
