using AutoMapper;
using UserService.Contracts.DTOs;
using UserService.Data.Entities;

namespace UserService.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();

        CreateMap<Image, ImageDto>();
        CreateMap<UserContacts, UserContactsDto>();
    }
}