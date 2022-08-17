using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly PropertyAppContext _context;

    public LikeRepository(PropertyAppContext appContext)
    {
        _context = appContext;
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
}
