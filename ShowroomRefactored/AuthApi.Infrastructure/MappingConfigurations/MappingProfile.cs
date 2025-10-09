using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AuthApi.Core.DTOs.Request;
using AuthApi.Core.Models;
using AutoMapper;

namespace AuthApi.Infrastructure.MappingConfigurations;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequestDTO, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore()); 
    }
}