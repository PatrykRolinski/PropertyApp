using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;

namespace PropertyApp.Application.Functions.Properties.Queries.GetPropertyDetail;

public class GetPropertyDetailHandler : IRequestHandler<GetPropertyDetailQuery, GetPropertyDetailDto>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public GetPropertyDetailHandler(IPropertyRepository propertyRepository,IMapper mapper )
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<GetPropertyDetailDto> Handle(GetPropertyDetailQuery request, CancellationToken cancellationToken)
    {
      var property=await _propertyRepository.GetByIdAsync(request.Id);
      var propertyDetail= _mapper.Map<GetPropertyDetailDto>(property);
       return propertyDetail;

    }
}
