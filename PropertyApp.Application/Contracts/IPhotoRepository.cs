
using Microsoft.AspNetCore.Http;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts;

public interface IPhotoRepository : IBaseRepository<Photo, int>
{
    public Task<Photo> GetPhotoForPropertyByIdAsync(int propertyId, int id);
    public Task<Photo> GetMainPhotoIdAsync(int propertyId);
    public Task AddPhotosToPropertyAsync(int propertyId, ICollection<Photo> photoFiles);
    public Task<IReadOnlyList<Photo>> GetPhotosForPropertyAsync(int propertyId);
}
