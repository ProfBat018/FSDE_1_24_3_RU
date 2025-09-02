using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AuthApi.Contracts.DTOs.Request;
using AuthApi.Data.Models;
using AutoMapper;

namespace AuthApi.Infrastructure.MappingConfigurations;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<RegisterRequestDTO, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => ValidateEmail(src.Email)))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => ValidatePassword(src.Password, src.ConfirmPassword)))
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore()); 
    }

    private static string ValidateEmail(string email)
    {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(email, emailPattern))
            throw new ValidationException("Invalid email format");
        return email;
    }

    private static string ValidatePassword(string password, string confirmPassword)
    {
        var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[_\W]).{8,}$";
        
        if (password != confirmPassword)
        {
            throw new ValidationException("Invalid password");
        }
        if (!Regex.IsMatch(password, passwordPattern))
            throw new ValidationException("Password must be at least 8 characters long, contain upper and lower case letters, and a digit");
        return password;
    }
}