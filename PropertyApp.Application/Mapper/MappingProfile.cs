using AutoMapper;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Mapper;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Property, GetPropertiesListDto>()
                .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country));
            

    }
}
