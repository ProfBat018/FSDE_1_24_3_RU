using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using UserService.API.DTOs.Requests;
using UserService.API.DTOs.Response;
using UserService.API.Services.Interfaces;
using UserService.Data.Data.Contexts;
using static BCrypt.Net.BCrypt;
namespace UserService.API.Services.Classes;

public class AuthService : IAuthService
{
    private readonly UserDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(UserDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<TypedResult<object>> LoginAsync(LoginRequestDTO request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || Verify(request.Password, user.Password) == false)
        {
            throw new InvalidCredentialException("Invalid Credentials");
        }

        var userRoles = await _context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Include(ur => ur.Role)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
            
        // var userRoles = await (
        //     from ur in _context.UserRoles
        //     join r in _context.Roles on ur.RoleId equals r.Id
        //     where ur.UserId == user.Id
        //     select r.Name
        // ).ToListAsync();

            

        var accessToken = await _tokenService.CreateTokenAsync(user, userRoles);

        return TypedResult<object>.Success(new
        {
            AccessToken = accessToken,
        }, "Successfully logged in");

    }
}