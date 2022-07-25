using PropertyApp.Domain.Enums;

namespace PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;

public class GetPropertiesListDto
{
    public int Price { get; set; }
    public PropertyType PropertyType { get; set; }
    public ushort PropertySize { get; set; }
    public byte NumberOfRooms { get; set; }
    public PropertyStatus PropertyStatus { get; set; }
    public string? MainPhotoUrl { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }

}
