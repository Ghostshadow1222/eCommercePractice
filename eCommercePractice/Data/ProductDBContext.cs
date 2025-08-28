using eCommercePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommercePractice.Data;

public class ProductDBContext : DbContext
{
    public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
    {

    }

    // Entites to be tracked by DbContext
    public DbSet<Product> Products { get; set; }
}
