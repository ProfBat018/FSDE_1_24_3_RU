using DocumentService.Data.Configurations;
using DocumentService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentService.Data;

public class DocumentDbContext : DbContext
{
    public DbSet<Document> Documents  { get; set; }
    public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        base.OnModelCreating(modelBuilder);
    }

}