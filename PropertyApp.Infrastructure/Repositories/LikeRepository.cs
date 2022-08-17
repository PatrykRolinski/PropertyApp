using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Repositories;

public class LikeRepository : BaseRepository<LikeProperty, int>, ILikeRepository
{
    public LikeRepository(PropertyAppContext context) : base(context)
    {
    }

    public IQueryable<Property> GetLikedPropertiesiesByUserQuery(Guid userId)
    {
        var properties = _context.Users.Join(_context.LikedProperties,
           u => u.Id,
           lp => lp.UserId, (users, likedProperties) => new { users, likedProperties })
           .Where(u => u.users.Id == userId)
           .Join(
           _context.Properties,
           combined => combined.likedProperties.PropertyId,
           p => p.Id, (combined, property) => property)
           .AsQueryable();

        return properties;

    }

    public async Task<bool> IsAlreadyLikedAsync(Guid userId, int propertyId)
    {
     var likeExsist=  await _context.LikedProperties.AnyAsync(lp=> lp.UserId == userId && lp.PropertyId == propertyId);
        return likeExsist;
    }

    public async Task<LikeProperty> GetByIdAsync(Guid userId, int propertyId)
    {
        // NOTE: Is Exceptions good Idea in repository?
        var result = await _context.LikedProperties.FindAsync(userId, propertyId);
        // if (result == null) throw new NotFoundException($"Item with {id} not found");
        return result;
    }

}
