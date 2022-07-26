using MediatR;
using PropertyApp.Application.Contracts;

namespace PropertyApp.Application.Functions.Properties.Commands.DeleteProperty;

public class DeletePropertyHandler : IRequestHandler<DeletePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;

    public DeletePropertyHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Unit> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
       var property=await  _propertyRepository.GetByIdAsync(request.PropertyId);
       await _propertyRepository.DeleteAsync(property);
       return Unit.Value;
    }
}
