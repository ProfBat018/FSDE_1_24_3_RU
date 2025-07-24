namespace UserService.Data.Data.Models;

public class UserRole
{
    public string Id { get; set; } =  Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string RoleId { get; set; }

    public Role Role { get; set; }
    public User User { get; set; }
}