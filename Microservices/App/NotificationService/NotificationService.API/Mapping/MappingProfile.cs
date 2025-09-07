using AutoMapper;
using NotificationService.Contracts.DTOs;
using NotificationService.Data.Entities;

namespace NotificationService.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Notification, NotificationDto>();
        CreateMap<CreateNotificationDto, Notification>();
    }
}