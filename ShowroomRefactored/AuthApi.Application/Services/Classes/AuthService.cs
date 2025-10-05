using System.Security.Authentication;
using AuthApi.Abstractions.Repos;
using AuthApi.Application.Services.Interfaces;
using AuthApi.Application.Utils;
using AuthApi.Core.DTOs.Request;
using AuthApi.Core.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static BCrypt.Net.BCrypt;

namespace AuthApi.Application.Services.Classes;


public class AuthService : IAuthService
{
    private readonly IUserDbContext _context;
    private readonly TokenManager _tokenManager;
    
    public AuthService(IUserDbContext context, TokenManager tokenManager)
    {
        _context = context;
        _tokenManager = tokenManager;
    }

    public async Task<TypedResult<string>> LoginAsync(LoginRequestDTO request)
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
        
        var accessToken = await _tokenManager.CreateTokenAsync(user, userRoles);

        return TypedResult<string>.Success(accessToken, "Successfully logged in");

    }
    
    public async Task<RefreshTokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken.ToString() == refreshToken);
        
        if(userFromDb == null || userFromDb.RefreshTokenExpiryTime <= DateTime.Now)
            throw new SecurityTokenException("Invalid token");

        var newToken = Guid.NewGuid();
        userFromDb.RefreshToken = newToken;
        userFromDb.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

        await _context.SaveChangesAsync();
        
        var userRoles = await _context.UserRoles
            .Where(ur => ur.UserId == userFromDb.Id)
            .Include(ur => ur.Role)
            .Select(ur => ur.Role.Name)
            .AsNoTracking()
            .ToListAsync();
        
        var accessToken = await _tokenManager.CreateTokenAsync(userFromDb, userRoles);
        
        return new RefreshTokenResponse(
            accessToken,
            newToken.ToString());
    }
}
