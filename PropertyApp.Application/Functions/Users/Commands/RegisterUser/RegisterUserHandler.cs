using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Services.EmailService;
using PropertyApp.Domain.Common;
using PropertyApp.Domain.Entities;
using System.Security.Cryptography;

namespace PropertyApp.Application.Functions.Users.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IOptions<EmailSettings> _emailConfig;
    private readonly IRoleRepository _roleRepository;

    public RegisterUserHandler(IPasswordHasher<User> passwordHasher, IUserRepository userRepository, 
        IEmailService emailService, IOptions<EmailSettings> emailConfig, IRoleRepository roleRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _emailService = emailService;
        _emailConfig = emailConfig;
        _roleRepository = roleRepository;
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
            RoleId =await _roleRepository.GetRoleId("Member"),
            //MemberId is default
            
            CreatedDate = DateTime.UtcNow,
            VerificationToken=CreateVerificationToken()
        };

        var emailDto = new EmailDto()
        {
            To = newUser.Email,
            Subject = "Verfify your account in PropertyApp",
            Body = $"Click to verify : http://localhost:4200/account/verify?token={newUser.VerificationToken}"
        };
       await _emailService.SendEmailAsync(emailDto, _emailConfig);

        var hashedPassword= _passwordHasher.HashPassword(newUser, request.Password);
      newUser.PasswordHash = hashedPassword;
       await _userRepository.AddAsync(newUser);       

        return Unit.Value;
    }
    private string CreateVerificationToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}
