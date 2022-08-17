using MediatR;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Likes.Commands.AddLike
{
    public class AddLikeHandler : IRequestHandler<AddLikeCommand>
    {
        private readonly ILikeRepository _likeRepository;
        private readonly ICurrentUserService _currentUser;

        public AddLikeHandler(ILikeRepository likeRepository, ICurrentUserService currentUser)
        {
            _likeRepository = likeRepository;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            if (userId == null)
            {
                throw new NotFoundException($"User with id {userId} is not found");
            }

          var likeExsist=await  _likeRepository.IsAlreadyLikedAsync(Guid.Parse(userId), request.PropertyId);
            if (likeExsist)
            {
                throw new LikeException("Property is already liked by You");
            }
            var like = new LikeProperty() { UserId = Guid.Parse(userId), PropertyId = request.PropertyId };
            await _likeRepository.AddAsync(like);

            return Unit.Value;
        }
    }
}