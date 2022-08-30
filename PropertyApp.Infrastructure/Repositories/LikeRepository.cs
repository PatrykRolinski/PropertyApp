using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Functions.Likes.Queries.GetLikedPropertiesList;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;
using System.Linq.Expressions;

namespace PropertyApp.Infrastructure.Repositories;

public class LikeRepository : BaseRepository<LikeProperty, int>, ILikeRepository
{
    public LikeRepository(PropertyAppContext context) : base(context)
    {
    }

    public async Task<PaginationHelper<Property>> GetLikedPropertiesiesByUserQuery(Guid userId, GetLikedPropertiesListQuery query)
    {
        var baseQuery = _context.Users.Join(_context.LikedProperties,
           u => u.Id,
           lp => lp.UserId, (users, likedProperties) => new { users, likedProperties })
           .Where(u => u.users.Id == userId)
           .Join(
           _context.Properties,
           combined => combined.likedProperties.PropertyId,
           p => p.Id, (combined, property) => property)
           .AsQueryable();


        baseQuery = baseQuery
             .Where(r => query.Country == null
             || r.Address.Country.ToLower().Contains(query.Country.ToLower()));

        baseQuery = baseQuery.Where(r => query.City == null || r.Address.City.ToLower().Contains(query.City.ToLower()));
        baseQuery = baseQuery.Where(r => query.MinimumPrice == 0 || r.Price >= query.MinimumPrice);
        baseQuery = baseQuery.Where(r => query.MaximumPrice == 0 || r.Price <= query.MaximumPrice);
        baseQuery = baseQuery.Where(r => query.MinimumSize == 0 || r.PropertySize >= query.MinimumSize);
        baseQuery = baseQuery.Where(r => query.MaximumSize == 0 || r.PropertySize <= query.MaximumSize);
        baseQuery = baseQuery.Where(r => query.PropertyStatus == null || r.PropertyStatus == query.PropertyStatus);
        baseQuery = baseQuery.Where(r => query.MarketType == null || r.MarketType == query.MarketType);
        baseQuery = baseQuery.Where(r => query.PropertyType == null || r.PropertyType == query.PropertyType);

        if (!string.IsNullOrEmpty(query.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Property, object>>>
           {
             {nameof(Property.Price), p=> p.Price},
             {"Date", p=> p.CreatedDate}

            };
            var selectedColumn = columnsSelector[query.SortBy];
            baseQuery = query.SortOrder == SortDirection.Ascending ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
        }



        var totalItems = baseQuery.Count();
        var properties =await baseQuery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToListAsync();


        var result = new PaginationHelper<Property>() { Items = properties, totalCount = totalItems };

        return result;

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
