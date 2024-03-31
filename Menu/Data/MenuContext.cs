using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.Data;

public class MenuContext(DbContextOptions<MenuContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DishIngredient>().HasKey(di => new
        {
            di.DishId,
            di.IngredientId,
        });

        modelBuilder.Entity<DishIngredient>()
            .HasOne(di => di.Dish)
            .WithMany(d => d.DishIngredients)
            .HasForeignKey(di => di.DishId);

        modelBuilder.Entity<DishIngredient>()
            .HasOne(di => di.Ingredient)
            .WithMany(i => i.DishIngredients)
            .HasForeignKey(di => di.IngredientId);

        modelBuilder.Entity<Dish>().HasData(new Dish()
            {
                Id = 1,
                Name = "Veg Fried Rice",
                Price = 240,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Frecipesofhome.com%2Fwp-content%2Fuploads%2F2020%2F06%2Fveg-fried-rice-recipe.jpg&f=1&nofb=1&ipt=74a23ad8b5848f1f5b77e870568291141c5d6855422e2bd5d7cce9467e8b1290&ipo=images",
            },
            new Dish()
            {
                Id = 2,
                Name = "Chili Chicken",
                Price = 180,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2F3.bp.blogspot.com%2F-SoqfR5Hoaec%2FV_7M52DiuiI%2FAAAAAAAABaw%2FH-aYSCKQ1jsgpZRUMgSUgC_F0BI9QT6hQCLcB%2Fs1600%2FChilli%252BChiken.jpg&f=1&nofb=1&ipt=60cd825dc4745a975b820d9846c6c328db849ddbe66882b3a052de082b9b3bfd&ipo=images",
            },
            new Dish()
            {
                Id = 3,
                Name = "Chicken Biryani",
                Price = 350,
                ImageUrl = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fcravingzone.com%2Fwp-content%2Fuploads%2F2020%2F07%2FPicsArt_07-27-08.42.12-1-scaled.jpg&f=1&nofb=1&ipt=f936d5de5990c561c85f33ae3dc7c6bfdb7d84c04de5ae23d5ecf2531352d05f&ipo=images"
            });

        modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
        {
            Id = 1,
            Name = "Kishmish"
        },
        new Ingredient()
        {
            Id = 2,
            Name = "Kaju"
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<DishIngredient> DishIngredients { get; set; }
}
