using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Repositories;

public class PropertyRepository : BaseRepository<Property, int>, IPropertyRepository
{
    public PropertyRepository(PropertyAppContext context) : base(context)
    {

    }

    public async Task<IReadOnlyList<Property>> GetPropertiesCreatedByUser(Guid userId)
    {
     var properties= await _context.Properties.Where(p => p.CreatedById == userId).ToListAsync();
      return properties;
    }
}
