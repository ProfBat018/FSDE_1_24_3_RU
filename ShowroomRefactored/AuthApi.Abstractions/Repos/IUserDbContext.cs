
using AuthApi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Abstractions.Repos;

public interface IUserDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<Role> Roles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}