using AutoMapper;
using ImageService.Contracts.DTOs;
using ImageService.Data.Entities;

namespace ImageService.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<StoredImage, StoredImageDto>();
    }
}