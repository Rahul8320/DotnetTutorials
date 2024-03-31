namespace Menu.Models;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double Price { get; set; }

    public List<DishIngredient>? DishIngredients { get; set; }
}
