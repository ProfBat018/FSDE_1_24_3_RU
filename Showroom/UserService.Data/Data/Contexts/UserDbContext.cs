using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserService.Data.Data.Models;

namespace UserService.Data.Data.Contexts;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options)  : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}