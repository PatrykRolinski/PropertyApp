using MediatR;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Messages.Queries.GetMessages
{
    public class GetMessagesQuery:IRequest<PageResult<MessageDto>>
    {
        public string? Container { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }  
        
    }
}