using AutoMapper;
using FluentValidation;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;
using System.Linq.Expressions;

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

        var paginationHelper = await _propertyRepository.GetAllAsync(request);
                     
        var propertiesDto= _mapper.Map<List<GetPropertiesListDto>>(paginationHelper.Items);

        var result = new PageResult<GetPropertiesListDto>(propertiesDto, request.PageNumber, paginationHelper.totalCount, request.PageSize);
        return result;
        
    }
}
