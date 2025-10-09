using AuthApi.Abstractions.Repos;
using AuthApi.Application.Services.Interfaces;
using AuthApi.Core.DTOs.Request;
using AuthApi.Core.DTOs.Response;
using AuthApi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Application.Services.Classes;


public class RoleService : IRoleService
{
    private readonly IUserDbContext _context;

    public RoleService(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<object>> GetAllRolesAsync(int page, int pageSize)
    {
        var allRolesCount = _context.Roles.Count();
        
        var roles =
            await _context.Roles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync();
        
        return PaginatedResult<object>.Success(roles, allRolesCount, page, pageSize);
    }

    public async Task<TypedResult<object>> GetRoleByIdAsync(string id)
    {
        Role? role = await _context.Roles.FindAsync(id);
        
        if (role == null)
        {
            throw new Exception("Role not found");
        }

        return TypedResult<object>.Success(new
        {
            Id = role.Id,
            Name = role.Name
        });
    }

    public async Task<Result> UpsertRoleAsync(UpsertRoleRequestDTO request)
    {
        string message; 
        var role = await _context.Roles.FindAsync(request.Id);

        if (request.Id != null && role != null)
        {
            role.Name = request.Name;
            message = "Role updated";
        }
        else
        {
            message = "Role added";
            await _context.Roles.AddAsync(new() { Name = request.Name });
        }

        await _context.SaveChangesAsync();
        return Result.Success(message);
    }

    public async Task<Result> RemoveRoleAsync(string id)
    {
        var role = await _context.Roles.FindAsync(id);

        if (role == null)
        {
            throw new Exception("Role not found");
        }
        
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return Result.Success("Role successfully deleted");
    }
}