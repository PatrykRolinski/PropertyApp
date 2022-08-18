using MediatR;

namespace PropertyApp.Application.Functions.Messages.Queries.GetMessageThread
{
    public class GetMessageThreadQuery:IRequest<GetMessageThreadDto>
    {
       public Guid SenderId { get; set; }
       public int PropertyId { get; set; }
    }
}