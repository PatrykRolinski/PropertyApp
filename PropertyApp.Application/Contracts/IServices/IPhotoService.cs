

using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts.IServices;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<ICollection<Photo>> AddPhotosAsync(ICollection<IFormFile> files);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}
