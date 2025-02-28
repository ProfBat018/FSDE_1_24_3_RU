using System.ComponentModel.DataAnnotations;

namespace LibProj.Data.Models;

public class Editor
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid PersonId { get; set; }
    public Person Person { get; set; }
    
    
    public ICollection<Book> Books { get; set; }
}