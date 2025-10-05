
using Microsoft.EntityFrameworkCore;
using UserService.API.Services.Interfaces;
using UserService.Data.Data.Contexts;

namespace UserService.API.Services.Classes;

public class UserService : IUserService
{
    private readonly UserDbContext _context;

    public UserService(UserDbContext context)
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