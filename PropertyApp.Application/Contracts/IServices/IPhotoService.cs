

using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace PropertyApp.Application.Contracts.IServices;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
}
