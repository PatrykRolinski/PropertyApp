using MediatR;

namespace PropertyApp.Application.Functions.Likes.Commands.DeleteLike;

public class DeleteLikeCommand: IRequest
{
   public int PropertyId { get; set; }
}
