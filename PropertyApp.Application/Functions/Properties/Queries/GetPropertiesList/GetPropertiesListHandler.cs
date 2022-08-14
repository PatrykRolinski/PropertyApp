using AutoMapper;
using FluentValidation;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;

public class GetPropertiesListHandler : IRequestHandler<GetPropertiesListQuery, PageResult<GetPropertiesListDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public GetPropertiesListHandler(IPropertyRepository propertyRepository, IMapper mapper)
    {
        
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<PageResult<GetPropertiesListDto>> Handle(GetPropertiesListQuery request, CancellationToken cancellationToken)
    {

        var validator = new GetPropertiesListValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var baseQuery = _propertyRepository.GetAllQuery()
              .Where(r => request.SearchPhrase == null
              || r.Address.City.ToLower().Contains(request.SearchPhrase.ToLower())
              || r.Address.Country.ToLower().Contains(request.SearchPhrase.ToLower()));

        var totalItems = baseQuery.Count();
        var properties = baseQuery
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToList();

        
        var propertiesDto= _mapper.Map<List<GetPropertiesListDto>>(properties);

        var result = new PageResult<GetPropertiesListDto>(propertiesDto, request.PageNumber, totalItems, request.PageSize);
        return result;
        
    }
}
