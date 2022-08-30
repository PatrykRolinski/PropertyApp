using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Users.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _userService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public ChangePasswordHandler(IUserRepository userRepository, ICurrentUserService userService, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _userService = userService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var validator = new ChangePasswordValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var userId = _userService.UserId;

        if (userId == null)
        {
            throw new NotFoundException($"User with {userId} id not found");
        }
       var user=await _userRepository.GetByIdAsync(Guid.Parse(userId));
       var hashedPassword= _passwordHasher.HashPassword(user, request.Password);
       user.PasswordHash=hashedPassword;
       await _userRepository.UpdateAsync(user);

        return Unit.Value;

        
    }
}
