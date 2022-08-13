using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Domain.Common;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Services.PhotoService;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;

    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var account = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult= new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(480).Width(640).Crop("fill")
            };
            uploadResult=await _cloudinary.UploadAsync(uploadParams);
        }
            return uploadResult;

    }
    public async Task<ICollection<Photo>> AddPhotosAsync(ICollection<IFormFile> files)
    {
        
         var photoFiles = new List<Photo>();
        if(files != null)
        {
            foreach (var file in files)
            {
             var result= await AddPhotoAsync(file);
              photoFiles.Add(new Photo() { Url = result.SecureUrl.AbsoluteUri, IsMain = false, PublicId = result.PublicId });
            }
        }
            return photoFiles;      

    }
    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var deletionResult =await _cloudinary.DestroyAsync(deletionParams);
        return deletionResult;
    }
     
    
}
