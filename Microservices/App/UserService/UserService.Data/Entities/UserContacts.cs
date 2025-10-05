namespace UserService.Data.Entities;

public class UserContacts : BaseEntity
{
    public string UserId { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneVerified { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
}