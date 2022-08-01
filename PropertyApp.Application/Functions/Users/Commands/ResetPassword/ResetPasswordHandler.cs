using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Users.Commands.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public ResetPasswordHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindyByEmail(request.Email);
        if (user == null || user.PasswordResetToken != request.Token || user.ResetTokenExpires < DateTime.UtcNow)
        {
            throw new ResetPasswordException("Something went wrong");
        }

       var password= _passwordHasher.HashPassword(user, request.Password);
       user.PasswordHash = password;
      await _userRepository.UpdateAsync(user);
       return Unit.Value;
    }
}
