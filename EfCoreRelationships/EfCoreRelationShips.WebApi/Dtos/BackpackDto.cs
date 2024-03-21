namespace EfCoreRelationShips.WebApi.Model.Dtos;

public class BackpackDto
{
    public required int Id { get; set; }
    public required string Description { get; set; }
    public required int CharacterId { get; set; }
    public required string CharacterName { get; set; }
}
