using MediatR;
using Microsoft.Extensions.Logging;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Properties.Commands.DeleteProperty;

public class DeletePropertyHandler : IRequestHandler<DeletePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly ILogger<DeletePropertyHandler> _logger;

    public DeletePropertyHandler(IPropertyRepository propertyRepository, ILogger<DeletePropertyHandler> logger)
    {
        _propertyRepository = propertyRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
       var property=await  _propertyRepository.GetByIdAsync(request.PropertyId);
        if (property == null)
        {
            throw new NotFoundException($"Property with {request.PropertyId} id not found");
        }
        await _propertyRepository.DeleteAsync(property);
        _logger.LogInformation($"Delete action invoked. Property with id {request.PropertyId} deleted.");
       return Unit.Value;
    }
}
