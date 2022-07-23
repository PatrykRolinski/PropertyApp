using PropertyApp.Domain.Common;
using PropertyApp.Domain.Enums;

namespace PropertyApp.Domain.Entities;

    public class Property : AuditableEntity
    {

    public int Id { get; set; }
    public string? Description { get; set; }
    public int OriginalPrice { get; set; }
    public int Price { get; set; }
    public PropertyType PropertyType { get; set; }
    public ushort PropertySize { get; set; }
    public byte NumberOfRooms { get; set; }
    public PropertyStatus PropertyStatus { get; set; }
    public MarketType MarketType { get; set; }
    public bool ClosedKitchen { get; set; }
    public Address? Address { get; set; }
    public int AddressId { get; set; }
    public ICollection<Photo>? Photos { get; set; }


    }

