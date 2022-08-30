using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Users.Queries.GetUser;

public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUser;

    public GetUserHandler(IUserRepository userRepository, IMapper mapper, IAuthorizationService authorizationService, ICurrentUserService currentUser)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
        {
            throw new NotFoundException($"User with {request.Id} id not found");
        }

        var authorizationResult =await  _authorizationService.AuthorizeAsync(_currentUser.User, user, new ResourceOperationRequirement(ResourceOperation.Read));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to read user with id {request.Id}");
        }



        var userDto = _mapper.Map<GetUserDto>(user);
        return userDto;

    }
}
