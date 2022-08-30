using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;
using PropertyApp.Application.Services.EmailService;
using PropertyApp.Domain.Common;
using System.Security.Cryptography;

namespace PropertyApp.Application.Functions.Users.Commands.ForgotPassword;

public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IOptions<EmailSettings> _emailConfig;
    private readonly IEmailService _emailService;

    public ForgotPasswordHandler(IUserRepository userRepository, IOptions<EmailSettings> emailConfig, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailConfig = emailConfig;
        _emailService = emailService;
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

        var emailDto = new EmailDto()
        {
            To = user.Email,
            Subject = "Change Password",
            Body = $"Your request to reset your password was submitted. If you did not make this request, simply ignore this email. " +
            $"If you did make this request just click the link below: : http://localhost:4200/account/reset-password?token={user.PasswordResetToken}"
        };

        await _emailService.SendEmailAsync(emailDto, _emailConfig); 

        return Unit.Value;
    }
    private string CreateVerificationToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}
