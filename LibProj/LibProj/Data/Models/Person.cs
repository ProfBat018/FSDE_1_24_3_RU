namespace LibProj.Data.Models;

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    
    public ICollection<Author> Authors { get; set; }
    public ICollection<Editor> Editors { get; set; }
}