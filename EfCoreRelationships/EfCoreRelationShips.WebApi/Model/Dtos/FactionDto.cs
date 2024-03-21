namespace EfCoreRelationShips.WebApi.Model.Dtos;

public class FactionDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public List<FactionCharacterDto> FactionCharacters { get; set; } = [];
}

public class FactionCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
