using System.ComponentModel.DataAnnotations;

namespace ContactService.Data.Entities;

public class Contact : BaseEntity
{

    public string UserId { get; set; } = default!;

    public string? PhoneNumber { get; set; }
    public bool PhoneVerified { get; set; }

    public string? Email { get; set; }
    public bool EmailVerified { get; set; }
}