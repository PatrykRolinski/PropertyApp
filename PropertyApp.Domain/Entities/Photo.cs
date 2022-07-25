namespace PropertyApp.Domain.Entities;

public class Photo
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }
    public virtual Property? Property { get; set; }
    public int PropertyId { get; set; }
}
