namespace EfCoreRelationShips.WebApi.Model.Dtos;

public class AddWeaponDto
{
    public required string Name { get; set; }
    public required int CharacterId { get; set; }
}
