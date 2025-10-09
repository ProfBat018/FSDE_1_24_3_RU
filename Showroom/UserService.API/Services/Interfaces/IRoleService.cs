using UserService.API.DTOs.Requests;
using UserService.API.DTOs.Response;
using UserService.Data.Data.Contexts;
using UserService.Data.Data.Models;

namespace UserService.API.Services.Interfaces;

public interface IRoleService
{
    public Task<PaginatedResult<object>> GetAllRolesAsync(int page, int pageSize);

    public Task<TypedResult<object>> GetRoleByIdAsync(string id);

    public Task<Result> UpsertRoleAsync(UpsertRoleRequestDTO request);

    public Task<Result> RemoveRoleAsync(string id);
}