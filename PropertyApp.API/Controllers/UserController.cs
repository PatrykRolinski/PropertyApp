using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Application.Functions.Users.Queries;

namespace PropertyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
         var usersListDto=  await _mediator.Send(new GetUsersListQuery());
           return Ok(usersListDto);

    }

    }
}
