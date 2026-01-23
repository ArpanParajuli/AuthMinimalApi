using Microsoft.EntityFrameworkCore;

using MinimalApi.Domain.Entities;
using MinimalApi.Domain.ValueObjects;

namespace MinimalApi.Infrastructure.Persistence;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Configure the User Entity
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            // Unique Guid for public identification
            builder.HasIndex(u => u.Pid).IsUnique();

            // 2. Map Email Value Object
            builder.Property(u => u.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value))
                .HasColumnName("Email")
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();

            // 3. Map Password Value Object
            builder.Property(u => u.Password)
                .HasConversion(
                    password => password.Value,
                    value => Password.Create(value))
                .HasColumnName("PasswordHash")
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}