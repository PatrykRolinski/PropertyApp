﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Properties.Commands.AddProperty;

public class CreatePropertyHandler : IRequestHandler<CreatePropertyCommand, int>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    private readonly ICurrentUserService _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public CreatePropertyHandler(IPropertyRepository propertyRepository,IMapper mapper, IPhotoService photoService,
        ICurrentUserService currentUser, IAuthorizationService authorizationService)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
        _photoService = photoService;
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var validator= new CreatePropertyValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

       var mappedProperty= _mapper.Map<Property>(request);

        var authorizationResult = await _authorizationService.AuthorizeAsync(_currentUser.User, mappedProperty, new ResourceOperationRequirement(ResourceOperation.Create));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to create property");
        }


        if (request.PhotoFile != null)
        {
            var result = await _photoService.AddPhotoAsync(request.PhotoFile);
            
            mappedProperty.Photos = new List<Photo>()
            { new Photo(){Url= result.SecureUrl.AbsoluteUri, IsMain=true, PublicId= result.PublicId}};

        }
        var userId = _currentUser.UserId;
        if (userId == null)
        {
            throw new NotFoundException($"User with id {userId} is not found");
        }

        mappedProperty.CreatedById = Guid.Parse(userId);
        mappedProperty.CreatedDate = DateTime.UtcNow;
        mappedProperty.OriginalPrice = mappedProperty.Price;
       var property=await _propertyRepository.AddAsync(mappedProperty);
        return property.Id;
    }
}
