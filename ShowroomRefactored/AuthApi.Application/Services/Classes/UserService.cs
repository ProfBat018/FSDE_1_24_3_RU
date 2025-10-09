using AuthApi.Abstractions.Repos;
using AuthApi.Application.Services.Interfaces;
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
        var res =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (res == null)
        {
            throw new Exception("User not found");
        }

        return res.Id;
    }
}