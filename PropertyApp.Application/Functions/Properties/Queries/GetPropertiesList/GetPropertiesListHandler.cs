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

        var baseQuery = _propertyRepository.GetAllQuery()
              .Where(r => request.Country == null             
              || r.Address.Country.ToLower().Contains(request.Country.ToLower()));

        baseQuery = baseQuery.Where(r => request.City == null || r.Address.City.ToLower().Contains(request.City.ToLower()));
        baseQuery = baseQuery.Where(r => request.MinimumPrice == 0 || r.Price>= request.MinimumPrice);
        baseQuery = baseQuery.Where(r => request.MaximumPrice == 0 || r.Price <= request.MaximumPrice);
        baseQuery = baseQuery.Where(r => request.MinimumSize == 0|| r.PropertySize >= request.MinimumSize);
        baseQuery = baseQuery.Where(r => request.MaximumSize == 0 || r.PropertySize <= request.MaximumSize);
        baseQuery = baseQuery.Where(r => request.PropertyStatus ==null || r.PropertyStatus == request.PropertyStatus);
        baseQuery = baseQuery.Where(r => request.MarketType == null || r.MarketType == request.MarketType);
        baseQuery = baseQuery.Where(r => request.PropertyType == null || r.PropertyType == request.PropertyType);

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Property, object>>>
           {
             {nameof(Property.Price), p=> p.Price},
             {"Date", p=> p.CreatedDate}

            };
            var selectedColumn = columnsSelector[request.SortBy];
            baseQuery= request.SortOrder== SortDirection.Ascending ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
        }

      



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
