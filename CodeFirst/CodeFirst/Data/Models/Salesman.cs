

using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Data.Models;

public class Salesman
{
    [Key]
    public int BadgeId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Salary { get; set; }

    public ICollection<Sale> Sales { get; set; }
}