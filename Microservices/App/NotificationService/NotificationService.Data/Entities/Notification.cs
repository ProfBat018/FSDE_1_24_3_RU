using System.ComponentModel.DataAnnotations;

namespace NotificationService.Data.Entities;

public class Notification : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public string UserId { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Message { get; set; } = default!;
    public bool IsRead { get; set; } = false;
}