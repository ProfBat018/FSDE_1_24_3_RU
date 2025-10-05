
using System.Globalization;
using System.Security.Claims;
using AuthApi.Abstractions.Repos;
using AuthApi.Application.Services.Interfaces;
using AuthApi.Core.DTOs.Request;
using AuthApi.Core.DTOs.Response;
using AuthApi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Application.Services.Classes;

public class UserService : IUserService
{
    private readonly IUserDbContext _context;
    
    public UserService(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<string> GetIdByEmailAsync(string email)
    {
        var res = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (res == null)
        {
            throw new Exception("User not found");
        }

        return res.Id;
    }

    public async Task<Result> AddUserInfoAsync(ClaimsPrincipal userClaims, UserInfoRequestDTO request)
    {
        if (userClaims == null)
            return Result.Error("Invalid user claims", 401);

        var email = userClaims.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
            return Result.Error("Email not found in claims", 401);

        var user = await _context.Users
            .Include(u => u.UserInfoTranslations)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return Result.Error("User not found", 404);

        // Обновляем базовое поле
        user.UserInfo = request.Info;

        // Создаём/обновляем переводы
        var translations = new List<UserInfoTranslations>
        {
            new UserInfoTranslations
            {
                TranslationId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                LanguageCode = "az",
                TranslatedInfo = request.InfoAz
            },
            new UserInfoTranslations
            {
                TranslationId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                LanguageCode = "ru",
                TranslatedInfo = request.InfoRu
            },
            new UserInfoTranslations
            {
                TranslationId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                LanguageCode = "en",
                TranslatedInfo = request.InfoEn
            }
        };

        // Очищаем старые переводы и добавляем новые
        user.UserInfoTranslations.Clear();
        foreach (var t in translations)
            user.UserInfoTranslations.Add(t);

        await _context.SaveChangesAsync();

        return Result.Success("User info added with translations");
    }

    public async Task<TypedResult<string>> GetUserInfoAsync(ClaimsPrincipal userClaims)
    {
        var email = userClaims.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
            return TypedResult<string>.Error("Email claim not found");

        var user = await _context.Users
            .Include(u => u.UserInfoTranslations)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return TypedResult<string>.Error("User not found");


        var info = user.UserInfo ?? string.Empty;

        return TypedResult<string>.Success(info);
    }
    

}