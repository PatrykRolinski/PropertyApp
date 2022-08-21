using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Models
{
    public class MessagePagination
    {
        public List<Message>? Messages { get; set; }
        public int totalCount { get; set; }

    }
}