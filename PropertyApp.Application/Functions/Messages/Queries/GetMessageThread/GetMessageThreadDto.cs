using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Messages.Queries.GetMessageThread;

public class GetMessageThreadDto
{
    public List<MessageDto>? Messages { get; set; }
    public int PropertyId { get; set; }

}
