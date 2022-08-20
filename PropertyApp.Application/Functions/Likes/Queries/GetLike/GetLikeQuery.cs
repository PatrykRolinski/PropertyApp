using MediatR;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Likes.Queries.GetLike;

public class GetLikeQuery : IRequest<bool>
{
    public int PropertyId { get; set; }
}
