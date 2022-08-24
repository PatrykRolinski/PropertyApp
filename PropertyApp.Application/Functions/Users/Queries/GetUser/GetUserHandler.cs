using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Users.Queries.GetUser;

public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
        {
            throw new NotFoundException($"User with {request.Id} id not found");
        }

        var userDto= _mapper.Map<GetUserDto>(user);
        return userDto;

    }
}
