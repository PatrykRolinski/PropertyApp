using PropertyApp.Domain.Entities;


namespace PropertyApp.Application.Contracts;

public interface IPropertyRepository : IBaseRepository<Property, int>
{
   public Task<IReadOnlyList<Property>> GetPropertiesCreatedByUser(Guid userId);
}
