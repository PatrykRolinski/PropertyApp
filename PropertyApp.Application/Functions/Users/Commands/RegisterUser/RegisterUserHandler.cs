using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Users.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new RegisterUserValidator(_userRepository);
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var newUser = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            //MemberId is default
            RoleId = 2,
            CreatedDate= DateTime.UtcNow,
        };

      var hashedPassword= _passwordHasher.HashPassword(newUser, request.Password);
      newUser.PasswordHash = hashedPassword;
       await _userRepository.AddAsync(newUser);       

        return Unit.Value;
    }
}
