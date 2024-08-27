using Microsoft.EntityFrameworkCore;

namespace VerticalSlice.Api.Products.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
