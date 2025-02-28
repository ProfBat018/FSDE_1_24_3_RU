namespace LibProj.Data.Models;

public class Publisher
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Name { get; set; }
    
    public ICollection<Book> Books { get; set; }
}