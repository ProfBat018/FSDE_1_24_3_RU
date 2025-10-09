using AutoMapper;
using ContactService.Contracts.DTOs;
using ContactService.Data.Entities;

namespace ContactService.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, ContactDto>();
        CreateMap<UpsertContactDto, Contact>();
    }
}