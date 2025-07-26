using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.API.DTOs.Requests;
using UserService.API.DTOs.Response;
using UserService.API.Services.Interfaces;
using UserService.Data.Data.Contexts;
using UserService.Data.Data.Models;
using static BCrypt.Net.BCrypt;

namespace UserService.API.Services.Classes;

public class AccountService : IAccountService
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;

    public AccountService(UserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result> RegisterAsync(RegisterRequestDTO request)
    {
        var userToAdd = _mapper.Map<User>(request);
        userToAdd.Password = HashPassword(request.Password);
        
        _context.Users.Add(userToAdd);

        await AssignRoleToUserAsync(userToAdd.Id);
        
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task AssignRoleToUserAsync(string userId, string roleName = "AppUser")
    {
        var role = await _context.Roles.FirstAsync(r => r.Name == roleName);

        _context.UserRoles.Add(new() { UserId = userId, RoleId = role.Id });
    }
}