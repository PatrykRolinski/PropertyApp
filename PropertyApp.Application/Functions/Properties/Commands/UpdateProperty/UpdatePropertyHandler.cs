using AutoMapper;
using FluentValidation;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;
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
       var validator = new UpdatePropertyValidator();
       await validator.ValidateAndThrowAsync(request, cancellationToken);

       var propertyToChange= await _propertyRepository.GetByIdAsync(request.Id);
        if (propertyToChange == null)
        {
            throw new NotFoundException($"Property with {request.Id} id not found");
        }
       var property= _mapper.Map<UpdatePropertyCommand, Property>(request, propertyToChange);

        property.LastModifiedDate = DateTime.UtcNow;
        
        await  _propertyRepository.UpdateAsync(property);
        return Unit.Value;
    }
}
