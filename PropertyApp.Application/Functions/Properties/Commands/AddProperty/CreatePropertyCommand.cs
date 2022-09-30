using MediatR;
using Microsoft.AspNetCore.Http;
using PropertyApp.Domain.Enums;

namespace PropertyApp.Application.Functions.Properties.Commands.AddProperty;

public class CreatePropertyCommand : IRequest<int>
{
    public string Description { get; set; }= string.Empty;
    public int OriginalPrice { get; set; }
    public int Price { get; set; }
    public PropertyType PropertyType { get; set; }
    public ushort PropertySize { get; set; }
    public byte NumberOfRooms { get; set; }
    public PropertyStatus PropertyStatus { get; set; }
    public MarketType MarketType { get; set; }
    public bool ClosedKitchen { get; set; } = false;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public byte? Floor { get; set; }
    public IFormFile? PhotoFile { get; set; }
    
}
