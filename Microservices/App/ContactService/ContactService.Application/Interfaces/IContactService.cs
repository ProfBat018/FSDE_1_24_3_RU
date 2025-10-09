
using ContactService.Contracts.DTOs;
using ContactService.Contracts.Response;

namespace ContactService.Application.Interfaces;

public interface IContactService
{
    Task<Result<ContactDto>> GetByUserIdAsync(string userId);
    Task<Result<ContactDto>> CreateOrUpdateAsync(string userId, UpsertContactDto dto);
    Task<Result<string>> DeleteAsync(string userId);
    Task<Result<string>> VerifyPhoneAsync(string userId);
    Task<Result<string>> VerifyEmailAsync(string userId);
}