using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;
using PropertyApp.Domain.Common;
using PropertyApp.Domain.Entities;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PropertyApp.Application.Functions.Users.Commands.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSetting _authenitcationSetting;

    public LoginUserHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, AuthenticationSetting authenitcationSetting)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _authenitcationSetting = authenitcationSetting;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new LoginUserValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userRepository.FindyByEmail(request.Email);
        if (user == null)
        {
            throw new ForbiddenException("Invalid email or password");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if(result== PasswordVerificationResult.Failed)
        {
            throw new ForbiddenException("Invalid email or password");
        }

        if (user.VerifiedAt == null)
        {
            throw new NotVerifiedException("You must verify your account, check email");
            
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.FirstName}"),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenitcationSetting.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(_authenitcationSetting.JwtExpireTime);

        var token = new JwtSecurityToken(_authenitcationSetting.JwtIssuer, _authenitcationSetting.JwtIssuer, 
            claims, expires: expires, signingCredentials: cred);

         var tokenHandler= new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

}
