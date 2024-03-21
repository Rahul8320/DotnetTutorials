namespace EfCoreRelationShips.WebApi.Model.Dtos;

public class CharacterDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public int? BackpackId  { get; set; }
    public string? BackpackDescription { get; set; }
    public List<CharacterWeaponDto> CharacterWeapons { get; set; } = [];
}

public class CharacterWeaponDto
{
    public int? WeaponId { get; set; }
    public string? WeaponName { get; set; }
}