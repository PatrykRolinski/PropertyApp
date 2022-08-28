using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PropertyApp.Application.Authorization;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Properties.Commands.DeleteProperty;

public class DeletePropertyHandler : IRequestHandler<DeletePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly ILogger<DeletePropertyHandler> _logger;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _currentUser;

    public DeletePropertyHandler(IPropertyRepository propertyRepository, 
        ILogger<DeletePropertyHandler> logger, IAuthorizationService authorizationService, ICurrentUserService currentUser)
    {
        _propertyRepository = propertyRepository;
        _logger = logger;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
       var property=await  _propertyRepository.GetByIdAsync(request.PropertyId);
        if (property == null)
        {
            throw new NotFoundException($"Property with {request.PropertyId} id not found");
        }

        var authorizationResult = await _authorizationService.AuthorizeAsync(_currentUser.User, property, new ResourceOperationRequirement(ResourceOperation.Update));
        if (!authorizationResult.Succeeded)
        {
            throw new ForbiddenException($"You do not have access to delete property with ID : {request.PropertyId}");
        }



        await _propertyRepository.DeleteAsync(property);
        _logger.LogInformation($"Delete action invoked. Property with id {request.PropertyId} deleted.");
       return Unit.Value;
    }
}
