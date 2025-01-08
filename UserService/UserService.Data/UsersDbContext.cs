namespace UserService.UserService.Data;

using global::UserService.UserService.Data.Models;
using Microsoft.EntityFrameworkCore;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    // Optional: override OnModelCreating if needed
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User table (you can add further configurations here)
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id); // Set primary key
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100); // Max length for Name
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(150); // Max length for Email
    }
}
