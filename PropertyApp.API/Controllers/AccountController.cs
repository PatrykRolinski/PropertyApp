using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Application.Functions.Users.Commands.LoginUser;
using PropertyApp.Application.Functions.Users.Commands.RegisterUser;
using PropertyApp.Application.Functions.Users.Commands.VerifyUser;

namespace PropertyApp.API.Controllers
{
    [Route("api/[controller]")]
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
            return Ok("Thank you for veryfing account, now you can log into your account");
        }
    }
}
