using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Photos.Commands.SetMainPhoto;

public class SetMainPhotoHandler : IRequestHandler<SetMainPhotoCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPhotoRepository _photoRepository;

    public SetMainPhotoHandler(IPropertyRepository propertyRepository, IPhotoRepository photoRepository)
    {
        _propertyRepository = propertyRepository;
        _photoRepository = photoRepository;
    }

    public async Task<Unit> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
    {
        var property = await _propertyRepository.GetByIdAsync(request.PropertyId);
        if (property == null)
        {
            throw new NotFoundException($"Property with {request.PropertyId} id not found");
        }
        var currentMainPhoto = await _photoRepository.GetMainPhotoIdAsync(request.PropertyId);

        var photo = await _photoRepository.GetPhotoByIdAsync(request.PropertyId, request.PhotoId);
        if(photo == null)
        {
            throw new NotFoundException($"Photo wiht {request.PhotoId} id for property with {request.PropertyId} id not found");
        }

       
        photo.IsMain = true;
       
       await _photoRepository.UpdateAsync(photo);
        if (currentMainPhoto != null)
        {
            
            currentMainPhoto.IsMain = false;
            await _photoRepository.UpdateAsync(currentMainPhoto);
        }

        return Unit.Value;





    }
}
