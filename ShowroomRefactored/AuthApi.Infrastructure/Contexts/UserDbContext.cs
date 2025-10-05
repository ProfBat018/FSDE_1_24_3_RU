using System.Reflection;
using AuthApi.Abstractions.Repos;
using AuthApi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Infrastructure.Contexts;

public class UserDbContext : DbContext, IUserDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    public DbSet<UserInfoTranslations> UserInfoTranslations { get; set; }
    
    public UserDbContext(DbContextOptions<UserDbContext> options)  : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}