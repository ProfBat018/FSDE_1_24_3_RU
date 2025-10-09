using System.Security.Claims;
using AuthApi.Application.Hubs;
using AuthApi.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AuthApi.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class TestController : ControllerBase
{
    private readonly IHubContext<NotificationHub> _notificationHub;

    public TestController(IHubContext<NotificationHub> notificationHub)
    {
        _notificationHub = notificationHub;
    }

    [HttpGet("api/test/secure")]
    public IActionResult GetSecureData()
    {
        return Ok("This is a secure endpoint. You are authenticated!");
    }

    [HttpPost("Notification")]
    public async Task<IActionResult> CreateNotificationAsync()
    {
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        await _notificationHub.Clients.User(id).SendAsync("ReceiveNotification", "This is a test notification.");    

        return Ok("Notification sent.");
    }
}