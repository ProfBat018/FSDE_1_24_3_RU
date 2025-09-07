

using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Interfaces;
using NotificationService.Contracts.DTOs;
using NotificationService.Contracts.Response;

namespace NotificationService.API.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<Result<IEnumerable<NotificationDto>>>> GetByUser(string userId)
    {
        var result = await _notificationService.GetByUserIdAsync(userId);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 400, result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<NotificationDto>>> Create(CreateNotificationDto dto)
    {
        var result = await _notificationService.CreateAsync(dto);
        return StatusCode(result.IsSuccess ? 201 : result.ErrorCode ?? 400, result);
    }

    [HttpPatch("{id}/read")]
    public async Task<ActionResult<Result<string>>> MarkAsRead(Guid id)
    {
        var result = await _notificationService.MarkAsReadAsync(id);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 404, result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<string>>> Delete(Guid id)
    {
        var result = await _notificationService.DeleteAsync(id);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 404, result);
    }
}