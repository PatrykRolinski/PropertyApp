
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts;

public interface IPhotoRepository : IBaseRepository<Photo, int>
{
    public Task<Photo> GetPhotoByIdAsync(int propertyId, int id);
    public Task<Photo> GetMainPhotoIdAsync(int propertyId);
}
