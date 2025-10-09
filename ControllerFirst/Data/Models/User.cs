namespace ControllerFirst.Data.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); 
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsConfirmed { get; set; }
}