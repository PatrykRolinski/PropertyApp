using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Users.Commands.ChangeRole;

public class ChangeRoleHandler : IRequestHandler<ChangeRoleCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeRoleHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new NotFoundException($"User with {request.UserId} id not found");
        }

       await _userRepository.ChangeUserRole(user, request.Role);
        //TODO: Throw exception
        return Unit.Value;
    }
}
