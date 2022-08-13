using MediatR;

namespace PropertyApp.Application.Functions.Photos.Queries;

public class GetPhotosListForPropertyQuery:IRequest<List<GetPhotosListForPropertyDto>>
{
    public int PropertyId { get; set; }
}
