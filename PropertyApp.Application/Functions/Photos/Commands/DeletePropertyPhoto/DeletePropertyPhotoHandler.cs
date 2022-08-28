using MediatR;
using Microsoft.AspNetCore.Authorization;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Photos.Commands.DeletePropertyPhoto;

public class DeletePropertyPhotoHandler : IRequestHandler<DeletePropertyPhotoCommand>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPhotoService _photoService;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUser;

    public DeletePropertyPhotoHandler(IPhotoRepository photoRepository, IPhotoService photoService, IPropertyRepository propertyRepository, 
        IAuthorizationService authorizationService, 
        ICurrentUserService currentUser)
    {
        _photoRepository = photoRepository;
        _photoService = photoService;
        _propertyRepository = propertyRepository;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(DeletePropertyPhotoCommand request, CancellationToken cancellationToken)
    {
        var property = await _propertyRepository.GetByIdAsync(request.PropertyId);
        if (property == null) throw new NotFoundException($"Property with {request.PropertyId} id not found");
         var photo=await  _photoRepository.GetPhotoForPropertyByIdAsync(request.PropertyId, request.PhotoId);
        
        if (photo == null ||    photo.PublicId == null)
        {
            throw new NotFoundException($"Photo with {request.PhotoId} id for property with {request.PropertyId} id not found");
        }
        var authorizationResult = await _authorizationService.AuthorizeAsync(_currentUser.User, property, new ResourceOperationRequirement(ResourceOperation.Delete));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to Add photos for property with ID : {request.PropertyId}");
        }
        await _photoService.DeletePhotoAsync(photo.PublicId);
      await _photoRepository.DeleteAsync(photo);

        return Unit.Value;
    }
}
