using AutoMapper;
using FluentValidation;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Users.Queries.GetUsersList;

public class GetUsersListHandler : IRequestHandler<GetUsersListQuery, PageResult<GetUsersListDto>>
{
    private readonly IUserRepository _userRespository;
    private readonly IMapper _mapper;

    public GetUsersListHandler(IUserRepository userRespository, IMapper mapper)
    {
        _userRespository = userRespository;
        _mapper = mapper;
    }

    public async Task<PageResult<GetUsersListDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {

        var validator = new GetUsersListValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var users=await _userRespository.GetAllAsync(request.SearchPhrase, request.PageSize, request.PageNumber);
        var usersDto=_mapper.Map<List<GetUsersListDto>>(users.Users);


        var result = new PageResult<GetUsersListDto>(usersDto, request.PageNumber, users.totalCount, request.PageSize);

        return result;



    }
}
