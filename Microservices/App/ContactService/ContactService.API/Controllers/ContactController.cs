using ContactService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ContactService.Contracts.DTOs;
using ContactService.Contracts.Response;

namespace ContactService.API.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<Result<ContactDto>>> Get(string userId)
    {
        var result = await _contactService.GetByUserIdAsync(userId);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 400, result);
    }

    [HttpPost("{userId}")]
    public async Task<ActionResult<Result<ContactDto>>> CreateOrUpdate(string userId, UpsertContactDto dto)
    {
        var result = await _contactService.CreateOrUpdateAsync(userId, dto);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 400, result);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<Result<string>>> Delete(string userId)
    {
        var result = await _contactService.DeleteAsync(userId);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 400, result);
    }

    [HttpPatch("{userId}/verify-phone")]
    public async Task<ActionResult<Result<string>>> VerifyPhone(string userId)
    {
        var result = await _contactService.VerifyPhoneAsync(userId);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 400, result);
    }

    [HttpPatch("{userId}/verify-email")]
    public async Task<ActionResult<Result<string>>> VerifyEmail(string userId)
    {
        var result = await _contactService.VerifyEmailAsync(userId);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 400, result);
    }
}
