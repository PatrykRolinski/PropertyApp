using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts.IServices;

public interface ILikeRepository: IBaseRepository<LikeProperty, int>
{
    public IQueryable<Property> GetLikedPropertiesiesByUserQuery(Guid userId);
    public Task<bool> IsAlreadyLikedAsync(Guid userId, int propertyId);
    public Task<LikeProperty> GetByIdAsync(Guid userId, int propertyId);
}
