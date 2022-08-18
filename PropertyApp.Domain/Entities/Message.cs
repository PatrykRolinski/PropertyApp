namespace PropertyApp.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int PropertyId { get; set; }
        public string? SenderFirstName { get; set; }
        public virtual User? Sender { get; set; }
        public Guid SenderId { get; set; }
        public string?  ReciepientFirstName { get; set; }
        public virtual User? Reciepient { get; set; }
        public Guid RecipientId { get; set; }

        public DateTime SendDate { get; set; }
        public DateTime? DateRead { get; set; }
        
    }
}