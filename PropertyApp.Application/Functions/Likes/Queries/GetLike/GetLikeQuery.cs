using MediatR;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Likes.Queries.GetLike;

public class GetLikeQuery : IRequest<GetLikeDto>
{
    public int PropertyId { get; set; }
}
