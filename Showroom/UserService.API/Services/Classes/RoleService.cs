using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserService.API.DTOs.Requests;
using UserService.API.DTOs.Response;
using UserService.API.Services.Interfaces;
using UserService.Data.Data.Contexts;
using UserService.Data.Data.Models;

namespace UserService.API.Services.Classes;

public class RoleService : IRoleService
{
    private readonly UserDbContext _context;

    public RoleService(UserDbContext context)
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