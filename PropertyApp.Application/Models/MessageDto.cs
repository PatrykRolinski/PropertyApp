namespace PropertyApp.Application.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int PropertyId { get; set; }
        public string? SenderFirstName { get; set; }        
        public Guid SenderId { get; set; }
        public string? ReciepientFirstName { get; set; }       
        public Guid RecipientId { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? DateRead { get; set; }
    }
}