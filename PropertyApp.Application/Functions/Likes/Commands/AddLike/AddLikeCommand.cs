using MediatR;

namespace PropertyApp.Application.Functions.Likes.Commands.AddLike;

public class AddLikeCommand : IRequest
{
    public int PropertyId { get; set; }
}
