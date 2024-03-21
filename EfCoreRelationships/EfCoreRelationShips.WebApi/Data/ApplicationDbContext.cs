using EfCoreRelationShips.WebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationShips.WebApi;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public DbSet<Faction> Factions { get; set; }
}
