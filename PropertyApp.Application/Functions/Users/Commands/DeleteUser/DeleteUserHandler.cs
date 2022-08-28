using MediatR;
using Microsoft.AspNetCore.Authorization;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Users.Commands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUser;

    public DeleteUserHandler(IUserRepository userRepository, IAuthorizationService authorizationService, ICurrentUserService currentUser)
    {
        _userRepository = userRepository;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user=  await _userRepository.GetByIdAsync(request.Id);
        
        if (user == null)
        {
            throw new NotFoundException($"User with {request.Id} id not found");
        }

        var authorizationResult = await _authorizationService.AuthorizeAsync(_currentUser.User, user, new ResourceOperationRequirement(ResourceOperation.Delete));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to delete user with id {request.Id}");
        }



        await _userRepository.DeleteAsync(user);
     
        return Unit.Value;
    }
}
