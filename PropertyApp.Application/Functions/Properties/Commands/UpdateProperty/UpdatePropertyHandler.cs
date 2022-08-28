using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Properties.Commands.UpdateProperty;

public class UpdatePropertyHandler : IRequestHandler<UpdatePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUser;

    public UpdatePropertyHandler(IPropertyRepository propertyRepository, IMapper mapper,
        IAuthorizationService authorizationService, ICurrentUserService currentUser)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
       var validator = new UpdatePropertyValidator();
       await validator.ValidateAndThrowAsync(request, cancellationToken);

       var propertyToChange= await _propertyRepository.GetByIdAsync(request.Id);
        if (propertyToChange == null)
        {
            throw new NotFoundException($"Property with {request.Id} id not found");
        }
        

       var property= _mapper.Map<UpdatePropertyCommand, Property>(request, propertyToChange);

      
        var authorizationResult =await _authorizationService.AuthorizeAsync(_currentUser.User, property, new ResourceOperationRequirement(ResourceOperation.Update));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to update property with ID : {request.Id}");
        }

        property.LastModifiedDate = DateTime.UtcNow;
        
        await  _propertyRepository.UpdateAsync(property);
        return Unit.Value;
    }
}
