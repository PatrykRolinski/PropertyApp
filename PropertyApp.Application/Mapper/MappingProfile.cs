using AutoMapper;
using PropertyApp.Application.Functions.Properties.Commands.AddProperty;
using PropertyApp.Application.Functions.Properties.Commands.UpdateProperty;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertiesList;
using PropertyApp.Application.Functions.Users.Queries.GetPropertiesListCreatedByUser;
using PropertyApp.Application.Functions.Properties.Queries.GetPropertyDetail;
using PropertyApp.Application.Functions.Users.Queries.GetUsersList;
using PropertyApp.Domain.Entities;
using PropertyApp.Application.Functions.Photos.Queries;
using PropertyApp.Application.Functions.Likes.Queries.GetLikedPropertiesList;
using PropertyApp.Application.Models;
using PropertyApp.Application.Functions.Users.Queries.GetUser;

namespace PropertyApp.Application.Mapper;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Property, GetPropertiesListDto>()
                .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src =>src.Photos==null? null: src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address ==null? null : src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address == null? null: src.Address.Country));


        CreateMap<Property, GetLikedProperiesListDto>()
                .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => src.Photos == null ? null : src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Country));


        CreateMap<CreatePropertyCommand, Property>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address() { City = src.City, Country = src.Country, Street = src.Street, Floor = src.Floor }));


        CreateMap<UpdatePropertyCommand, Property>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address() { City = src.City, Country = src.Country, Street = src.Street, Floor = src.Floor }));
            

        CreateMap<Photo, PhotoDto>();

        CreateMap<Photo, GetPhotosListForPropertyDto>();

        CreateMap<Property, GetPropertyDetailDto>()
             .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => src.Photos == null ? null : src.Photos.FirstOrDefault(x => x.IsMain).Url))
             .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
              .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Country))
             .ForMember(dest=> dest.Floor, opt=> opt.MapFrom(src => src.Address == null ? null : src.Address.Floor))
             .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
             .ForMember(dest=> dest.CreatedById, opt=> opt.MapFrom(src=> src.CreatedById));

        CreateMap<Property, GetPropertiesListCreatedByUserDto>()
             .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => src.Photos == null ? null : src.Photos.FirstOrDefault(x => x.IsMain).Url))
             .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
             .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Country))             
             .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street));

        CreateMap<Message, MessageDto>();


        CreateMap<User, GetUsersListDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

        CreateMap<User, GetUserDto>()
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
    }
}
