using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;


namespace PropertyApp.Application.Contracts;

public interface IPropertyRepository : IBaseRepository<Property, int>
{
   public Task<IReadOnlyList<Property>> GetPropertiesCreatedByUser(Guid userId);
    public Task<PaginationHelper<Property>> GetAllAsync(GetPropertiesListQuery query);
}
