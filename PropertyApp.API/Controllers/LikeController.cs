using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.API.Extensions;
using PropertyApp.Application.Functions.Likes.Queries.GetLikedPropertiesList;

namespace PropertyApp.API.Controllers;
//TODO: userid:?
[Route("api/user")]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LikeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("liked-property")]
    public async Task<ActionResult<List<GetLikedProperiesListDto>>> GetLikedPropertiesByUser([FromQuery] GetLikedPropertiesListQuery query)
    {
        var result=  await _mediator.Send(query);
        Response.AddPaginationHeader(query.PageNumber, query.PageSize, result.TotalCount, result.TotalPages, result.ItemsFrom, result.ItemsTo);
        return Ok(result.Items);
    }
}
