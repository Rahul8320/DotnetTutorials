namespace EfCoreRelationShips.WebApi.Model;

public class Faction
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Character> Characters { get; set; } = [];
}
