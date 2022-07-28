using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;

namespace PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;

public class GetPropertiesListHandler : IRequestHandler<GetPropertiesListQuery, List<GetPropertiesListDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public GetPropertiesListHandler(IPropertyRepository propertyRepository, IMapper mapper)
    {
        
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<List<GetPropertiesListDto>> Handle(GetPropertiesListQuery request, CancellationToken cancellationToken)
    {
        var all = await _propertyRepository.GetAllAsync();
        return _mapper.Map<List<GetPropertiesListDto>>(all);        
    }
}
