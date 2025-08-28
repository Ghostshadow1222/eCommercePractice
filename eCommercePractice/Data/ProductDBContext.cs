using eCommercePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommercePractice.Data;

public class ProductDBContext : DbContext
{
    public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Two different ways to set unique index
        modelBuilder.Entity<Member>() // Ensure the member usernames are unique
            .HasIndex(nameof(Member.Username))
            .IsUnique();

        modelBuilder.Entity<Member>() // Ensure the member emails are unique
            .HasIndex(m => m.Email)
            .IsUnique();
    }

    // Entites to be tracked by DbContext
    public DbSet<Product> Products { get; set; }

    public DbSet<Member> Members { get; set; }
}
