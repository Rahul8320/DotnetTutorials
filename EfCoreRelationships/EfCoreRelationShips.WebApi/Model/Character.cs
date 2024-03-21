using EfCoreRelationShips.WebApi.Model;

namespace EfCoreRelationShips.WebApi;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public Backpack Backpack { get; set; } = default!;
    public List<Weapon> Weapons { get; set; } = [];
}
