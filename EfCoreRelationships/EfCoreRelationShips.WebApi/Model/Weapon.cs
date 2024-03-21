namespace EfCoreRelationShips.WebApi.Model;

public class Weapon
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int CharacterId { get; set; }
    public Character Character { get; set; } = default!;
}
