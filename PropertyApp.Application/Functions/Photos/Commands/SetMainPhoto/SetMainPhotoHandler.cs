using MediatR;
using Microsoft.AspNetCore.Authorization;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Photos.Commands.SetMainPhoto;

public class SetMainPhotoHandler : IRequestHandler<SetMainPhotoCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUser;

    public SetMainPhotoHandler(IPropertyRepository propertyRepository, IPhotoRepository photoRepository, 
        IAuthorizationService authorizationService, ICurrentUserService currentUser)
    {
        _propertyRepository = propertyRepository;
        _photoRepository = photoRepository;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
    {
        var property = await _propertyRepository.GetByIdAsync(request.PropertyId);
        if (property == null)
        {
            throw new NotFoundException($"Property with {request.PropertyId} id not found");
        }

        var authorizationResult = await _authorizationService.AuthorizeAsync(_currentUser.User, property, new ResourceOperationRequirement(ResourceOperation.Update));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to change main photo for property with ID : {request.PropertyId}");
        }


        var currentMainPhoto = await _photoRepository.GetMainPhotoIdAsync(request.PropertyId);

        var photo = await _photoRepository.GetPhotoForPropertyByIdAsync(request.PropertyId, request.PhotoId);
        if(photo == null)
        {
            throw new NotFoundException($"Photo with {request.PhotoId} id for property with {request.PropertyId} id not found");
        }

        if (photo.Id == currentMainPhoto.Id)
        {
            throw new Exception("This is actually your main photo");
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
