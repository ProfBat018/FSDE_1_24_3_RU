using AutoMapper;
using DocumentService.Contracts.DTOs;
using DocumentService.Contracts.Enums;
using DocumentService.Data.Entities;

namespace DocumentService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DocumentUploadDto, Document>()
            .ForMember(dest => dest.OriginalFileName, opt => opt.MapFrom(src => src.File.FileName))
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.File.ContentType))
            .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.File.Length))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => DocumentStatus.Pending))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.File.Length))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.StoredFileName, opt => opt.Ignore())
            .ForMember(dest => dest.InternalUrl, opt => opt.Ignore())
            .ForMember(dest => dest.UploadedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastAccessedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ExpiresAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
