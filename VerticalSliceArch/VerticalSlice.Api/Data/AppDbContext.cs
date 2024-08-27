using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Products.Entity;
using VerticalSlice.Api.Users;

namespace VerticalSlice.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
}
