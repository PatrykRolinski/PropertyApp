using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.API.Extensions;
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
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<GetUsersListDto>>> GetAllUsers([FromQuery] GetUsersListQuery query)
        {
            var list = await _mediator.Send(query);
            Response.AddPaginationHeader(query.PageNumber, query.PageSize, list.TotalCount, list.TotalPages, list.ItemsFrom, list.ItemsTo);
            return Ok(list.Items);

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
        [Authorize(Roles ="Admin")]
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
