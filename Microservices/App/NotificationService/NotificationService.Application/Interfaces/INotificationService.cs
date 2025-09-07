using NotificationService.Contracts.DTOs;
using NotificationService.Contracts.Response;

namespace NotificationService.Application.Interfaces;


public interface INotificationService
{
    Task<Result<NotificationDto>> CreateAsync(CreateNotificationDto dto);
    Task<Result<IEnumerable<NotificationDto>>> GetByUserIdAsync(string userId);
    Task<Result<string>> MarkAsReadAsync(Guid id);
    Task<Result<string>> DeleteAsync(Guid id);
}