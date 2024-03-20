namespace EfCoreRelationShips.WebApi;

public class Backpack
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public int CharacterId { get; set; }
    public Character Character { get; set; } = default!;
}
