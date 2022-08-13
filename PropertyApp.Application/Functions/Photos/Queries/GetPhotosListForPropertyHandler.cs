using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Photos.Queries;

public class GetPhotosListForPropertyHandler : IRequestHandler<GetPhotosListForPropertyQuery, List<GetPhotosListForPropertyDto>>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public GetPhotosListForPropertyHandler(IPhotoRepository photoRepository,IPropertyRepository propertyRepository, IMapper mapper)
    {
        _photoRepository = photoRepository;
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<List<GetPhotosListForPropertyDto>> Handle(GetPhotosListForPropertyQuery request, CancellationToken cancellationToken)
    {
        var property = await _propertyRepository.GetByIdAsync(request.PropertyId);
       if (property == null)
        {
            throw new NotFoundException($"Property with {request.PropertyId} id not found");
        }
     var photosLists= await  _photoRepository.GetPhotosForPropertyAsync(request.PropertyId);
        if (photosLists == null)
        {
            throw new NotFoundException($"Photos for property with {request.PropertyId} id not found");
        }

        var photosListDto= _mapper.Map<List<GetPhotosListForPropertyDto>>(photosLists);
       return photosListDto;
    }
}
