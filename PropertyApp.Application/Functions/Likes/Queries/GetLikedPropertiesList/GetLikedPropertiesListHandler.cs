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

        
        var paginationHelper = await _likeRepository.GetLikedPropertiesiesByUserQuery(Guid.Parse(userId), request);
        
        var propertiesListDto = _mapper.Map<List<GetLikedProperiesListDto>>(paginationHelper.Items);

        var result = new PageResult<GetLikedProperiesListDto>(propertiesListDto, request.PageNumber, paginationHelper.totalCount, request.PageSize);
        return await Task.FromResult(result);
    }
}
