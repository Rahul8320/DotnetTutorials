namespace Menu.Models;

public class DishIngredient
{
    public int DishId { get; set; }
    public Dish Dish { get; set; } = default!;

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = default!;
}
