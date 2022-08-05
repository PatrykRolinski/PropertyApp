using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Repositories
{
    public class PhotoRepository : BaseRepository<Photo, int>, IPhotoRepository
    {
        public PhotoRepository(PropertyAppContext context) : base(context)
        {
        }

        public async Task<Photo> GetPhotoByIdAsync(int propertyId, int id)
        {
            var photo =await _context.Photos.FirstOrDefaultAsync(x => x.PropertyId == propertyId && x.Id == id);
            return photo;
        }
        public async Task<Photo> GetMainPhotoIdAsync(int propertyId)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.PropertyId == propertyId && x.IsMain==true);
            return photo;
        }
    }
}