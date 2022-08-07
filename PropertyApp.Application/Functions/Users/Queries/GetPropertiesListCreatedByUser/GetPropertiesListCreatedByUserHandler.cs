using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Users.Queries.GetPropertiesListCreatedByUser;

public class GetPropertiesListCreatedByUserHandler : IRequestHandler<GetPropertiesListCreatedByUserQuery, List<GetPropertiesListCreatedByUserDto>>
{
    private readonly IPropertyRepository _propertyrepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetPropertiesListCreatedByUserHandler(IPropertyRepository propertyrepository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _propertyrepository = propertyrepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<List<GetPropertiesListCreatedByUserDto>> Handle(GetPropertiesListCreatedByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if(userId == null)
        {
            throw new NotFoundException($"User with id {userId} is not found");
        }
      
    
      var properties=await _propertyrepository.GetPropertiesCreatedByUser(Guid.Parse(userId));
      var propertiesDto= _mapper.Map<List<GetPropertiesListCreatedByUserDto>>(properties);
       return propertiesDto;
    }
}
