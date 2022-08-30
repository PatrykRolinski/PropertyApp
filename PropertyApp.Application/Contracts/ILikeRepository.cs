using PropertyApp.Application.Functions.Likes.Queries.GetLikedPropertiesList;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts.IServices;

public interface ILikeRepository: IBaseRepository<LikeProperty, int>
{
    public Task<PaginationHelper<Property>> GetLikedPropertiesiesByUserQuery(Guid userId, GetLikedPropertiesListQuery query);
    public Task<bool> IsAlreadyLikedAsync(Guid userId, int propertyId);
    public Task<LikeProperty> GetByIdAsync(Guid userId, int propertyId);
}
