using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Photos.Commands.DeletePropertyPhoto;

public class DeletePropertyPhotoHandler : IRequestHandler<DeletePropertyPhotoCommand>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPhotoService _photoService;

    public DeletePropertyPhotoHandler(IPhotoRepository photoRepository, IPhotoService photoService)
    {
        _photoRepository = photoRepository;
        _photoService = photoService;
    }

    public async Task<Unit> Handle(DeletePropertyPhotoCommand request, CancellationToken cancellationToken)
    {

      var photo=await  _photoRepository.GetPhotoForPropertyByIdAsync(request.PropertyId, request.PhotoId);
        
        if (photo == null)
        {
            throw new NotFoundException($"Photo with {request.PhotoId} id for property with {request.PropertyId} id not found");
        }

      await _photoService.DeletePhotoAsync(photo.PublicId);
      await _photoRepository.DeleteAsync(photo);

        return Unit.Value;
    }
}
