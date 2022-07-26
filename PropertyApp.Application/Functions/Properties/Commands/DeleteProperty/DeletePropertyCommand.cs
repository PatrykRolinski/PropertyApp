using MediatR;

namespace PropertyApp.Application.Functions.Properties.Commands.DeleteProperty;

public class DeletePropertyCommand : IRequest
{
    public int PropertyId { get; set; }
}
