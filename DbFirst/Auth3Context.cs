using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbFirst;

public partial class Auth3Context : DbContext
{
    public Auth3Context()
    {
    }

    public Auth3Context(DbContextOptions<Auth3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
            .GetConnectionString("Default");
        
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleName).HasName("PK__Roles__B19478608AC74D53");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Users__66DCF95D74433D39");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61649FAC056B").IsUnique();

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.IsEmailConfirmed)
                .HasDefaultValue(false)
                .HasColumnName("isEmailConfirmed");
            entity.Property(e => e.Password).HasColumnName("password");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__CD3149CC5F0665E7");

            entity.Property(e => e.UserRoleId).HasColumnName("userRoleId");
            entity.Property(e => e.RoleNameRef)
                .HasMaxLength(50)
                .HasColumnName("roleNameRef");
            entity.Property(e => e.UserNameRef)
                .HasMaxLength(50)
                .HasColumnName("userNameRef");

            entity.HasOne(d => d.RoleNameRefNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleNameRef)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__roleN__34C8D9D1");

            entity.HasOne(d => d.UserNameRefNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserNameRef)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__userN__33D4B598");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}