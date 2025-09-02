using System.Security.Claims;
using System.Text;
using AuthApi.Application.Services.Interfaces;
using AuthApi.Contracts.DTOs.Request;
using AuthApi.Contracts.DTOs.Response;
using AuthApi.Data.Models;
using AuthApi.Infrastructure.Contexts;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt;

namespace AuthApi.Application.Services.Classes;


public class AccountService : IAccountService
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly EmailSender _emailSender;

    public AccountService(UserDbContext context, IMapper mapper, IWebHostEnvironment env, EmailSender emailSender)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
        _emailSender = emailSender;
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

    public async Task ConfirmEmailAsync(ClaimsPrincipal userClaims, string token, HttpContext context)
    {
        var email = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
        
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        var filePath = Path.Combine(_env.WebRootPath, "ConfirmMessage.html");

        var messageContent = new StringBuilder(await File.ReadAllTextAsync(filePath));

        var link = $"{context.Request.Scheme}://{context.Request.Host}/api/Account/Verify/{user.Id}/{token}";
        
        messageContent.Replace("{User}", user.Name);
        messageContent.Replace("{ConfirmationLink}", link);
        
        await _emailSender.SendEmailAsync(user.Email, "Confirm your email", messageContent.ToString());
    }

    public async Task<Result> VerifyEmailAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);
        user.IsConfirmed = true;
        await _context.SaveChangesAsync();
        
        return Result.Success("Email confirmed");
    }


    public async Task AssignRoleToUserAsync(string userId, string roleName = "AppUser")
    {
        var role = await _context.Roles.FirstAsync(r => r.Name == roleName);

        _context.UserRoles.Add(new() { UserId = userId, RoleId = role.Id });
    }
}