namespace PropertyApp.Application.Functions.Users.Queries.GetPropertiesListCreatedByUser;

public class GetPropertiesListCreatedByUserDto
{
    public int Id { get; set; }
    public int Price { get; set; }    
    public string? MainPhotoUrl { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
}
