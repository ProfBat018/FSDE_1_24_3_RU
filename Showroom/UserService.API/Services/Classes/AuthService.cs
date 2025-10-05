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
        

        var accessToken = await _tokenService.CreateTokenAsync(user, userRoles);
       
        user.RefreshToken = Guid.NewGuid().ToString();
        user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();
        
        return TypedResult<object>.Success(new
        {
            AccessToken = accessToken,
            RefreshToken = user.RefreshToken,
        }, "Successfully logged in");

    }
}