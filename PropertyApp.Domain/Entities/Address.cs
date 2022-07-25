namespace PropertyApp.Domain.Entities;

public class Address
{
    public int Id { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public byte? Floor { get; set; }
    public virtual Property? Property { get; set; }

}
