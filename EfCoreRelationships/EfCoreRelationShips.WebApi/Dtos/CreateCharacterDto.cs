namespace EfCoreRelationShips.WebApi.Dtos;

public record struct CreateCharacterDto(
    string Name,
    CreateBackpackDto BackpackDto,
    List<CreateWeaponDto> WeaponDtos,
    List<CreateFactionDto> FactionDtos
);

public record struct CreateBackpackDto(string Description);

public record struct CreateWeaponDto(string Name);

public record struct CreateFactionDto(string Name);