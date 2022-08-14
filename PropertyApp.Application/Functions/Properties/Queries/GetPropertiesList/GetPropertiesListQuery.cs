using MediatR;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;

public class GetPropertiesListQuery : IRequest<PageResult<GetPropertiesListDto>>
{
    public string? SearchPhrase { get; set; }    
    
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
