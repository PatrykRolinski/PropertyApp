using FluentValidation;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;
using System.Security.Cryptography;

namespace PropertyApp.Application.Functions.Users.Commands.ForgotPassword;

public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ForgotPasswordHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
       var validator= new ForgotPasswordValidator();
       await validator.ValidateAndThrowAsync(request, cancellationToken);

        var user= await _userRepository.FindyByEmail(request.Email);
       
        if(user == null)
        {
            throw new NotFoundException("User not found");
        }

        user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
        user.PasswordResetToken = CreateVerificationToken();
        await _userRepository.UpdateAsync(user); 
        return Unit.Value;
    }
    private string CreateVerificationToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}
