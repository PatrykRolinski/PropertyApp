namespace PropertyApp.Domain.Entities;

public class LikeProperty
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
    public int PropertyId { get; set; }
    public virtual Property? Property { get; set; }

}
