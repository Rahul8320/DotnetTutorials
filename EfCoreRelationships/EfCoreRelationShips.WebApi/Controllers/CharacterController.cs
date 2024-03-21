using EfCoreRelationShips.WebApi.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreRelationShips.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

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
            }).ToList()
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
