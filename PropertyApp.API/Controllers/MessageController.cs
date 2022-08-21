using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.API.Extensions;
using PropertyApp.Application.Functions.Messages.Commands.AddMessage;
using PropertyApp.Application.Functions.Messages.Queries.GetMessages;
using PropertyApp.Application.Functions.Messages.Queries.GetMessageThread;
using PropertyApp.Application.Models;

namespace PropertyApp.API.Controllers
{
    [Route("api/user/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddMessage([FromBody] AddMessageCommand messageCommand )
        {
          var messageId=await _mediator.Send(messageCommand);
          return Created($"api/user/message/{messageId}", null);
        }
        [HttpGet("thread")]
        public async Task<ActionResult<GetMessageThreadDto>> GetMessageThread([FromQuery] string senderId, [FromQuery] int propertyId)
        {
           var messageThread= await _mediator.Send(new GetMessageThreadQuery() { PropertyId=propertyId, SenderId=Guid.Parse(senderId)});
           return Ok(messageThread);
        }
        [HttpGet]
        public async Task<ActionResult<List<MessageDto>>> GetMessages([FromQuery] GetMessagesQuery messageQuery) 
        {
           var list=await _mediator.Send(messageQuery);
           Response.AddPaginationHeader(messageQuery.PageNumber, messageQuery.PageSize, list.TotalCount, list.TotalPages, list.ItemsFrom, list.ItemsTo);            
           return Ok(list.Items);
        }
    }
}
