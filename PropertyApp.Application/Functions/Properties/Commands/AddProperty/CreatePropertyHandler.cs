using AutoMapper;
using FluentValidation;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Properties.Commands.AddProperty;

public class CreatePropertyHandler : IRequestHandler<CreatePropertyCommand, int>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoservice;

    public CreatePropertyHandler(IPropertyRepository propertyRepository,IMapper mapper, IPhotoService photoservice)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
        _photoservice = photoservice;
    }

    public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var validator= new CreatePropertyValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);


       

       var mappedProperty= _mapper.Map<Property>(request);

        if (request.PhotoFile != null)
        {
            var result = await _photoservice.AddPhotoAsync(request.PhotoFile);
            
            mappedProperty.Photos = new List<Photo>()
            { new Photo(){Url= result.SecureUrl.AbsoluteUri, IsMain=true, PublicId= result.PublicId}};

        }

        // To do Add UserId
        mappedProperty.CreatedById = Guid.Parse("925900AA-96F4-4617-EF74-08DA76507A32");
        mappedProperty.CreatedDate = DateTime.UtcNow;
        mappedProperty.OriginalPrice = mappedProperty.Price;
       var property=await _propertyRepository.AddAsync(mappedProperty);
        return property.Id;
    }
}
