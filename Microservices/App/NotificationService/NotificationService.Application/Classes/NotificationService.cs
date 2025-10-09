

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Interfaces;
using NotificationService.Contracts.DTOs;
using NotificationService.Contracts.Response;
using NotificationService.Data;
using NotificationService.Data.Entities;

namespace NotificationService.Application.Services;

public class NotificationService : INotificationService
{
    private readonly NotificationDbContext _context;
    private readonly IMapper _mapper;

    public NotificationService(NotificationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<NotificationDto>> CreateAsync(CreateNotificationDto dto)
    {
        var notification = _mapper.Map<Notification>(dto);
        notification.Id = Guid.NewGuid();

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        return Result<NotificationDto>.Success(_mapper.Map<NotificationDto>(notification));
    }

    public async Task<Result<IEnumerable<NotificationDto>>> GetByUserIdAsync(string userId)
    {
        var notifications = await _context.Notifications
            .AsNoTracking()
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        var result = _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        return Result<IEnumerable<NotificationDto>>.Success(result);
    }

    public async Task<Result<string>> MarkAsReadAsync(Guid id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null)
            return Result<string>.Error("Notification not found", 404);

        notification.IsRead = true;
        await _context.SaveChangesAsync();

        return Result<string>.Success("Marked as read");
    }

    public async Task<Result<string>> DeleteAsync(Guid id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null)
            return Result<string>.Error("Notification not found", 404);

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();

        return Result<string>.Success("Notification deleted");
    }
}
