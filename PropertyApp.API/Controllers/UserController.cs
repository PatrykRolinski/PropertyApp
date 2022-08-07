using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Application.Functions.Users.Queries.GetPropertiesListCreatedByUser;
using PropertyApp.Application.Functions.Users.Queries.GetUsersList;

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
    public async Task<ActionResult<List<GetUsersListDto>>> GetAllUsers()
    {
         var usersListDto=  await _mediator.Send(new GetUsersListQuery());
           return Ok(usersListDto);

    }
    [HttpGet("created-properties")]
     public async Task<ActionResult<List<GetPropertiesListCreatedByUserDto>>> GetCreatedPropertiesByUser()
    {
        var propertyCreatedByUser= await _mediator.Send(new GetPropertiesListCreatedByUserQuery());
         return Ok(propertyCreatedByUser);
    }


    }
}
