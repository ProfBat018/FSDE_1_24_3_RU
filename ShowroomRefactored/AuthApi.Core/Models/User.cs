
namespace AuthApi.Core.Models;


public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsConfirmed { get; set; } = false;
    public Guid? RefreshToken { get; set; } = null;
    public DateTime? RefreshTokenExpiryTime { get; set; } = null;
    public List<UserRole> UserRoles { get; set; }
}