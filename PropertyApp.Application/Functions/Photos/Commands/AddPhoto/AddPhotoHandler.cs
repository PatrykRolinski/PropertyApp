using MediatR;
using Microsoft.AspNetCore.Authorization;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Photos.Commands.AddPhoto;

public class AddPhotoHandler : IRequestHandler<AddPhotoCommand>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPhotoService _photoService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUser;

    public AddPhotoHandler(IPhotoRepository photoRepository, IPropertyRepository propertyRepository, IPhotoService photoservice,
        IAuthorizationService authorizationService, ICurrentUserService currentUser)
    {
        _photoRepository = photoRepository;
        _propertyRepository = propertyRepository;
        _photoService = photoservice;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
      var property= await _propertyRepository.GetByIdAsync(request.PropertyId);
        if(property == null)
        {
            throw new NotFoundException($"Property with {request.PropertyId} id not found");
        }

        var authorizationResult = await _authorizationService.AuthorizeAsync(_currentUser.User, property, new ResourceOperationRequirement(ResourceOperation.Update));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to Add photos for property with ID : {request.PropertyId}");
        }


        if (request.PhotoFiles == null)
        {
            throw new Exception("Something wrong with photo files ");
        }

        var photoList= await _photoService.AddPhotosAsync(request.PhotoFiles);

        await _photoRepository.AddPhotosToPropertyAsync(request.PropertyId,photoList);

        return Unit.Value;
    }
}
