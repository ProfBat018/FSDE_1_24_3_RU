namespace UserService.Data.Data.Models;

public class Role
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
}