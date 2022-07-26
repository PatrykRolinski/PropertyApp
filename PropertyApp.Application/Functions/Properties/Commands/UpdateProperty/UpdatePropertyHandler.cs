using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Properties.Commands.UpdateProperty;

public class UpdatePropertyHandler : IRequestHandler<UpdatePropertyCommand>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public UpdatePropertyHandler(IPropertyRepository propertyRepository, IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
       var propertyToChange= await _propertyRepository.GetByIdAsync(request.Id);
       var property= _mapper.Map<UpdatePropertyCommand, Property>(request, propertyToChange);
        await  _propertyRepository.UpdateAsync(property);
        return Unit.Value;
    }
}
