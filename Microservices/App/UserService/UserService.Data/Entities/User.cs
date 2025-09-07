namespace UserService.Data.Entities;

public class User : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    public Guid ImageId { get; set; }
    public Image Image { get; set; }

    public UserContacts Contacts { get; set; }
}