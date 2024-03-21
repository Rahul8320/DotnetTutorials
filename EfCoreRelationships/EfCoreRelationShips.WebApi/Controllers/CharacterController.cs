using EfCoreRelationShips.WebApi.Dtos;
using EfCoreRelationShips.WebApi.Model;
using EfCoreRelationShips.WebApi.Model.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationShips.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CharacterDto>> GetCharacter([FromRoute] int id)
    {
        try
        {
            var character = await _context.Characters.Select(item => new CharacterDto() {
                Id = item.Id,
                Name = item.Name,
                BackpackId = item.Backpack.Id,
                BackpackDescription = item.Backpack.Description,
                CharacterWeapons = item.Weapons.Select(weapon => new CharacterWeaponDto(){
                    WeaponId = weapon.Id,
                    WeaponName = weapon.Name,
                }).ToList(),
                CharacterFactions = item.Factions.Select(fiction => new CharacterFactionDto(){
                    Id = fiction.Id,
                    Name = fiction.Name,
                }).ToList(),
            }).FirstOrDefaultAsync(c => c.Id == id);
    
            return Ok(character);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {
                ex.Message,
                ErrorCode = StatusCodes.Status500InternalServerError,
                ex.StackTrace,
                ex.Source
            });
        }
    }

    [HttpPost("create-character")]
    public async Task<ActionResult<Character>> CreateCharacter([FromBody] CreateCharacterDto request)
    {
        try
        {
            var character = new Character { Name = request.Name };
            var backpack = new Backpack { Description = request.BackpackDto.Description, Character = character };
            var weapons = request.WeaponDtos.Select(weapon => new Weapon{ Name = weapon.Name, Character = character }).ToList();
            var factions = request.FactionDtos.Select(faction => new Faction{ Name = faction.Name, Characters = [character]}).ToList();

            character.Backpack = backpack;
            character.Weapons = weapons;
            character.Factions = factions;

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, character);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {
                ex.Message,
                ErrorCode = StatusCodes.Status500InternalServerError,
                ex.StackTrace,
                ex.Source
            });
        }
    }


    [HttpGet]
    public ActionResult<List<CharacterDto>> GetAll()
    {
        var allCharacters = _context.Characters.Select(item => new CharacterDto() {
            Id = item.Id,
            Name = item.Name,
            BackpackId = item.Backpack.Id,
            BackpackDescription = item.Backpack.Description,
            CharacterWeapons = item.Weapons.Select(weapon => new CharacterWeaponDto(){
                WeaponId = weapon.Id,
                WeaponName = weapon.Name,
            }).ToList(),
            CharacterFactions = item.Factions.Select(fiction => new CharacterFactionDto(){
                Id = fiction.Id,
                Name = fiction.Name,
            }).ToList(),
        }).ToList();

        return Ok(allCharacters);
    }

    [HttpPost]
    public IActionResult Create([FromBody] string name)
    {
        var character = new Character()
        {
            Name = name,
        };
        
        _context.Characters.Add(character);
        _context.SaveChanges();

        return StatusCode(StatusCodes.Status201Created, character);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var character = _context.Characters.FirstOrDefault(c => c.Id == id);

        if(character is null)
        {
            return NotFound();
        }

        _context.Remove(character);
        _context.SaveChanges();

        return NoContent();
    }
}
