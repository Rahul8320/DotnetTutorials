namespace Menu.Models;

public class Ingredient
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;

    public List<DishIngredient>? DishIngredients { get; set; }
}
