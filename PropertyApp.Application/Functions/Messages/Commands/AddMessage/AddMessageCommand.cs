using MediatR;

namespace PropertyApp.Application.Functions.Messages.Commands.AddMessage;

public class AddMessageCommand: IRequest<int>
{
    public Guid RecipientId { get; set; }
    public string? Content { get; set; }
    public int PropertyId { get; set; }    
}
