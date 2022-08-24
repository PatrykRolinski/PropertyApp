using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Application.Functions.Users.Commands.ChangeRole;
using PropertyApp.Application.Functions.Users.Commands.DeleteUser;
using PropertyApp.Application.Functions.Users.Commands.UpdateUser;
using PropertyApp.Application.Functions.Users.Queries.GetPropertiesListCreatedByUser;
using PropertyApp.Application.Functions.Users.Queries.GetUser;
using PropertyApp.Application.Functions.Users.Queries.GetUsersList;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Enums;

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
            var usersListDto = await _mediator.Send(new GetUsersListQuery());
            return Ok(usersListDto);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUser([FromRoute] Guid id)
        {
            var UserDto = await _mediator.Send(new GetUserQuery() { Id = id });
            return Ok(UserDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUser updateUser)
        {
            await _mediator.Send(new UpdateUserCommand() { UserId = id, FirstName = updateUser.FirstName, LastName = updateUser.LastName });
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteUserCommand() { Id = id });
            return NoContent();
        }

        [HttpPut("{id}/role")]
        public async Task<ActionResult> ChangeRole([FromRoute]Guid id, [FromBody] RoleName roleName)
        {
            await _mediator.Send(new ChangeRoleCommand { UserId = id, Role = roleName });
            return NoContent();
        }


    [HttpGet("created-properties")]
     public async Task<ActionResult<List<GetPropertiesListCreatedByUserDto>>> GetCreatedPropertiesByUser()
    {
        var propertyCreatedByUser= await _mediator.Send(new GetPropertiesListCreatedByUserQuery());
         return Ok(propertyCreatedByUser);
    }


    }
}
