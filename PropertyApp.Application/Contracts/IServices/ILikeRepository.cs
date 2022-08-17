using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts.IServices;

public interface ILikeRepository
{
    public IQueryable<Property> GetLikedPropertiesiesByUserQuery(Guid userId);
}
