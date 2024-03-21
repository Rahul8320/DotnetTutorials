namespace EfCoreRelationShips.WebApi.Model.Dtos;

public class WeaponDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int CharacterId { get; set; }
    public required string CharacterName { get; set; }
}
