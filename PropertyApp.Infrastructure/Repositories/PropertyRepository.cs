using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;
using System.Linq.Expressions;

namespace PropertyApp.Infrastructure.Repositories;

public class PropertyRepository : BaseRepository<Property, int>, IPropertyRepository
{
    public PropertyRepository(PropertyAppContext context) : base(context)
    {

    }

    public async Task<IReadOnlyList<Property>> GetPropertiesCreatedByUser(Guid userId)
    {
        var properties = await _context.Properties.Where(p => p.CreatedById == userId).ToListAsync();
        return properties;
    }
    public async Task<PaginationHelper<Property>> GetAllAsync(GetPropertiesListQuery query)
    {
       var baseQuery= _context.Properties.AsQueryable().Where(r => query.Country == null
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

        var result= new PaginationHelper<Property>() { Items=properties, totalCount=totalItems};
        return result;
    }
}
