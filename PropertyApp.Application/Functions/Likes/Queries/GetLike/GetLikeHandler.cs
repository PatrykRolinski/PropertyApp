using MediatR;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Likes.Queries.GetLike;

public class GetLikeHandler : IRequestHandler<GetLikeQuery, bool>
{
    private readonly ILikeRepository _likeRepository;
    private readonly ICurrentUserService _currentUser;

    public GetLikeHandler(ILikeRepository likeRepository, ICurrentUserService currentUser)
    {
        _likeRepository = likeRepository;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(GetLikeQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        if (userId == null)
        {
            throw new NotFoundException($"User with id {userId} is not found");
        }


        var like=  await _likeRepository.GetByIdAsync(Guid.Parse(userId), request.PropertyId);
        
        if (like == null)
        {
            return false;
        }

        return true;
    }
}
