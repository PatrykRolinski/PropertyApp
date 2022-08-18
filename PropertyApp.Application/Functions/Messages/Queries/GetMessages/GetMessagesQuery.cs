using MediatR;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Messages.Queries.GetMessages
{
    public class GetMessagesQuery:IRequest<List<MessageDto>>
    {
        public string? Container { get; set; }
    }
}