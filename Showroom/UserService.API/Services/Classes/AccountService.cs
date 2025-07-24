using UserService.API.Services.Interfaces;
using UserService.Data.Data.Contexts;
using Bcrypt = BCrypt.Net.BCrypt;
namespace UserService.API.Services.Classes;

public class AccountService : IAccountService
{
    private readonly UserDbContext _context;

    public AccountService(UserDbContext context)
    {
        _context = context;
    }

    public Task<Result> RegisterAsync()
    {
        throw new NotImplementedException();
    }
}