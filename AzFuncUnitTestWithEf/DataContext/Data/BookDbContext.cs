using DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Data;

public class BookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
        if (options != null)
        {
            return;
        }

        throw new ArgumentNullException(nameof(options));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=MyLocalDb.db");
        }
    }
}
