using MediatR;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Likes.Commands.DeleteLike;

public class DeleteLikeHandler : IRequestHandler<DeleteLikeCommand>
{
    private readonly ILikeRepository _likeRepository;
    private readonly ICurrentUserService _currentUser;

    public DeleteLikeHandler(ILikeRepository likeRepository, ICurrentUserService currentUser)
    {
        _likeRepository = likeRepository;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        if (userId == null)
        {
            throw new NotFoundException($"User with id {userId} is not found");
        }

        var like =await _likeRepository.GetByIdAsync(Guid.Parse(userId), request.PropertyId);
        
        if (like == null)
        {
            throw new NotFoundException($"Like for {request.PropertyId} id was not found");
        }

      await _likeRepository.DeleteAsync(like);

       return Unit.Value;
    }
}
