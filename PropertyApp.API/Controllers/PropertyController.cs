using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.API.Extensions;
using PropertyApp.Application.Functions.Properties.Commands.AddProperty;
using PropertyApp.Application.Functions.Properties.Commands.DeleteProperty;
using PropertyApp.Application.Functions.Properties.Commands.UpdateProperty;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertyDetail;

namespace PropertyApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly IMediator _mediator;

    public PropertyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetPropertiesListDto>>> GetAllProperties([FromQuery] GetPropertiesListQuery query )
    {
        var list = await _mediator.Send(query);
        Response.AddPaginationHeader(query.PageNumber, query.PageSize, list.TotalCount, list.TotalPages, list.ItemsFrom, list.ItemsTo);
        return Ok(list.Items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetPropertyDetailDto>> GetProperty([FromRoute]int id)
    {
        var propertyDto = await _mediator.Send(new GetPropertyDetailQuery() { Id = id });
        return Ok(propertyDto);
    }
    [HttpPost]
    public async Task<ActionResult> AddProperty([FromForm] CreatePropertyCommand createPropertyCommand)
    {
       
        var id = await _mediator.Send(createPropertyCommand);
        return Created($"/api/property/{id}", null);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProperty([FromRoute] int id)
    {
        var deletePropertyCommand = new DeletePropertyCommand() { PropertyId = id };
        await _mediator.Send(deletePropertyCommand);
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProperty([FromRoute] int id, [FromBody] UpdatePropertyCommand updatePropertyCommand)
    {
        updatePropertyCommand.Id=id;
        await _mediator.Send(updatePropertyCommand);
        return NoContent();
    }
}
