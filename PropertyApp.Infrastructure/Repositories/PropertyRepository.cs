using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Repositories;

public class PropertyRepository : BaseRepository<Property, int>, IPropertyRepository
{
    public PropertyRepository(PropertyAppContext context) : base(context)
    {
    }
}
