using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Data.Models;

public class CarType
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string CarTypeName { get; set; }
}