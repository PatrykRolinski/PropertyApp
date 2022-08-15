using MediatR;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Enums;

namespace PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;

public class GetPropertiesListQuery : IRequest<PageResult<GetPropertiesListDto>>
{
      
    public string? Country { get; set; }
    public string? City { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int MinimumPrice { get; set; }
    public int MaximumPrice { get; set; }
    public int MinimumSize { get; set; }
    public int MaximumSize { get; set; }
    public PropertyStatus? PropertyStatus { get; set; }
    public MarketType? MarketType { get; set; }
    public PropertyType? PropertyType { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortOrder { get; set; }
}
