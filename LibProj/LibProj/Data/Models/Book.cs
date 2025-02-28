namespace LibProj.Data.Models;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    public string Description { get; set; }
    public int PageCount { get; set; }
    
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
    
    public Guid PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public Guid EditorId { get; set; }
    public Editor Editor { get; set; }
}