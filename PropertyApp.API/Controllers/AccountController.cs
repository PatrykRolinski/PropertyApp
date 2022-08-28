using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Application.Functions.Users.Commands.ForgotPassword;
using PropertyApp.Application.Functions.Users.Commands.LoginUser;
using PropertyApp.Application.Functions.Users.Commands.RegisterUser;
using PropertyApp.Application.Functions.Users.Commands.ResetPassword;
using PropertyApp.Application.Functions.Users.Commands.VerifyUser;

namespace PropertyApp.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody]RegisterUserCommand registerUserCommand)
        {
           await _mediator.Send(registerUserCommand);
           return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserCommand loginUserCommand)
        {
          var token= await _mediator.Send(loginUserCommand);
            return Ok(token);
        }
        [HttpPost("verify")]
        public async Task<ActionResult> VerifyUser([FromQuery] string token)
        {
            await _mediator.Send(new VerifyUserCommand { Token=token});
            return Ok();
        }
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] string email)
        {
            await _mediator.Send(new ForgotPasswordCommand(){Email=email});
            return Ok("Now you can change the password");
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromQuery]string token, [FromBody] ResetPasswordDto resetPasswordDto)
        {
            await _mediator.Send(new ResetPasswordCommand {Token=token, Email=resetPasswordDto.Email, 
                Password=resetPasswordDto.Password, ConfirmPassword=resetPasswordDto.ConfirmPassword });
            return Ok("Your password has been changed");
        }
    }
}
