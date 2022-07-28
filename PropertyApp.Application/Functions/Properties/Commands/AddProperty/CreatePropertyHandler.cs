using AutoMapper;
using FluentValidation;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Properties.Commands.AddProperty;

public class CreatePropertyHandler : IRequestHandler<CreatePropertyCommand, int>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public CreatePropertyHandler(IPropertyRepository propertyRepository,IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var validator= new CreatePropertyValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

       var mappedProperty= _mapper.Map<Property>(request);
        // To do Add UserId
        mappedProperty.CreatedById = Guid.Parse("5F4AF149-A2BD-416F-34AB-08DA6D872DB6");
        mappedProperty.CreatedDate = DateTime.UtcNow;
        mappedProperty.OriginalPrice = mappedProperty.Price;
       var property=await _propertyRepository.AddAsync(mappedProperty);
        return property.Id;
    }
}
