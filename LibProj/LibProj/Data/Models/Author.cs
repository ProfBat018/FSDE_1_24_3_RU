namespace LibProj.Data.Models;

public class Author
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PersonId { get; set; }
    public Person Person { get; set; }
    public int Rating { get; set; }
    
    public ICollection<Book> Books { get; set; }
}