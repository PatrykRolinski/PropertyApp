using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Users.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user=  await _userRepository.GetByIdAsync(request.Id);
        
        if (user == null)
        {
            throw new NotFoundException($"User with {request.Id} id not found");
        }
         await _userRepository.DeleteAsync(user);
     
        return Unit.Value;
    }
}
