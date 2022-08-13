using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Photos.Commands.AddPhoto;

public class AddPhotoHandler : IRequestHandler<AddPhotoCommand>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPhotoService _photoService;

    public AddPhotoHandler(IPhotoRepository photoRepository, IPropertyRepository propertyRepository, IPhotoService photoservice)
    {
        _photoRepository = photoRepository;
        _propertyRepository = propertyRepository;
        _photoService = photoservice;
    }

    public async Task<Unit> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
      var propery= await _propertyRepository.GetByIdAsync(request.PropertyId);
    if(propery == null)
        {
            throw new NotFoundException($"Property with {request.PropertyId} id not found");
        }
    if(request.PhotoFiles == null)
        {
            throw new Exception("Something wrong with photo files ");
        }

    var photoList= await _photoService.AddPhotosAsync(request.PhotoFiles);

     await _photoRepository.AddPhotosToPropertyAsync(request.PropertyId,photoList);

        return Unit.Value;
    }
}
