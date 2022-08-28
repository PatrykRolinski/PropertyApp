using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUser;

    public UpdateUserHandler(IUserRepository userRepository, IAuthorizationService authorizationService, 
        ICurrentUserService currentUser)
    {
        _userRepository = userRepository;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserValidator(_userRepository);
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var user =await _userRepository.GetByIdAsync(request.UserId);


        var authorizationResult = await _authorizationService.AuthorizeAsync(_currentUser.User, user, new ResourceOperationRequirement(ResourceOperation.Update));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to update user with id {request.UserId}");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}
