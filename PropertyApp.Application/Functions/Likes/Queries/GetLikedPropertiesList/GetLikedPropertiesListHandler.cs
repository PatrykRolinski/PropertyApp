using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;
using System.Linq.Expressions;

namespace PropertyApp.Application.Functions.Likes.Queries.GetLikedPropertiesList;

public class GetLikedPropertiesListHandler : IRequestHandler<GetLikedPropertiesListQuery, PageResult<GetLikedProperiesListDto>>
{
    private readonly ILikeRepository _likeRepository;
    private readonly ICurrentUserService _userService;
    private readonly IMapper _mapper;

    public GetLikedPropertiesListHandler(ILikeRepository likeRepository, ICurrentUserService userService, IMapper mapper)
    {
        _likeRepository = likeRepository;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<PageResult<GetLikedProperiesListDto>> Handle(GetLikedPropertiesListQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.UserId;
        if (userId == null)
        {
            throw new NotFoundException($"User with id {userId} is not found");
        }

        // TODO: Need to refatctor this method
        var baseQuery = _likeRepository.GetLikedPropertiesiesByUserQuery(Guid.Parse(userId));
        baseQuery= baseQuery
             .Where(r => request.Country == null
             || r.Address.Country.ToLower().Contains(request.Country.ToLower()));

        baseQuery = baseQuery.Where(r => request.City == null || r.Address.City.ToLower().Contains(request.City.ToLower()));
        baseQuery = baseQuery.Where(r => request.MinimumPrice == 0 || r.Price >= request.MinimumPrice);
        baseQuery = baseQuery.Where(r => request.MaximumPrice == 0 || r.Price <= request.MaximumPrice);
        baseQuery = baseQuery.Where(r => request.MinimumSize == 0 || r.PropertySize >= request.MinimumSize);
        baseQuery = baseQuery.Where(r => request.MaximumSize == 0 || r.PropertySize <= request.MaximumSize);
        baseQuery = baseQuery.Where(r => request.PropertyStatus == null || r.PropertyStatus == request.PropertyStatus);
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
            baseQuery = request.SortOrder == SortDirection.Ascending ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
        }



        var totalItems = baseQuery.Count();
        var properties = baseQuery
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToList();

        var propertiesListDto = _mapper.Map<List<GetLikedProperiesListDto>>(properties);

        var result = new PageResult<GetLikedProperiesListDto>(propertiesListDto, request.PageNumber, totalItems, request.PageSize);
        return await Task.FromResult(result);
    }
}
