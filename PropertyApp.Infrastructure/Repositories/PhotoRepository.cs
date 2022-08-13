using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Repositories
{
    public class PhotoRepository : BaseRepository<Photo, int>, IPhotoRepository
    {
        
        public PhotoRepository(PropertyAppContext context) : base(context)
        {
            
        }

        public async Task<Photo> GetPhotoForPropertyByIdAsync(int propertyId, int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.PropertyId == propertyId && p.Id == id);
            return photo;
        }
        public async Task<IReadOnlyList<Photo>> GetPhotosForPropertyAsync(int propertyId)
        {
         var photosList= await  _context.Photos.Where(p => p.PropertyId == propertyId).ToListAsync();
          return photosList;
        }
        public async Task<Photo> GetMainPhotoIdAsync(int propertyId)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.PropertyId == propertyId && p.IsMain == true);
            return photo;
        }
        public async Task AddPhotosToPropertyAsync(int propertyId, ICollection<Photo> photoFiles)
        {
                       
            foreach(Photo photo in photoFiles)
            {
                photo.PropertyId=propertyId;
            }
            await _context.Photos.AddRangeAsync(photoFiles);
            await _context.SaveChangesAsync();
        }
    }
}