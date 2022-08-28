using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.API.Extensions;
using PropertyApp.Application.Functions.Likes.Commands.AddLike;
using PropertyApp.Application.Functions.Likes.Commands.DeleteLike;
using PropertyApp.Application.Functions.Likes.Queries.GetLike;
using PropertyApp.Application.Functions.Likes.Queries.GetLikedPropertiesList;
using PropertyApp.Domain.Entities;

namespace PropertyApp.API.Controllers;

[Route("api/User")]
[Authorize]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LikeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("likes")]
    public async Task<ActionResult<List<GetLikedProperiesListDto>>> GetLikedPropertiesByUser([FromQuery] GetLikedPropertiesListQuery query)
    {
        var result=  await _mediator.Send(query);
        Response.AddPaginationHeader(query.PageNumber, query.PageSize, result.TotalCount, result.TotalPages, result.ItemsFrom, result.ItemsTo);
        return Ok(result.Items);
    }
    [HttpPost("property/{propertyId}/like")]
    public async Task<ActionResult> AddLike([FromRoute] int propertyId)
    {
      var likeId =await _mediator.Send(new AddLikeCommand() { PropertyId = propertyId });
      return Created($"api/user/property/{propertyId}/like", null);
    }

    [HttpDelete("property/{propertyId}/like")]
    public async Task<ActionResult> UnLike([FromRoute] int propertyId)
    {
        await _mediator.Send(new DeleteLikeCommand() { PropertyId = propertyId });
        return NoContent();
    }
    [HttpGet("property/{propertyId}/like")]
    public async Task<ActionResult<bool>> GetLike([FromRoute] int propertyId)
    {
      var like= await _mediator.Send(new GetLikeQuery() { PropertyId=propertyId });
       return like;
    }
}
