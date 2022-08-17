using MediatR;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Enums;

namespace PropertyApp.Application.Functions.Likes.Queries.GetLikedPropertiesList;
 public class GetLikedPropertiesListQuery: IRequest<PageResult<GetLikedProperiesListDto>>
    {
    public string? Country { get; set; }
    public string? City { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
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

