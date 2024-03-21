using EfCoreRelationShips.WebApi.Model;
using EfCoreRelationShips.WebApi.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreRelationShips.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FactionController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public ActionResult<List<FactionDto>> GetAll()
    {
        var allFaction = _context.Factions.Select(item => new FactionDto(){
            Id = item.Id,
            Name = item.Name,
            FactionCharacters = item.Characters.Select(character => new FactionCharacterDto{
                Id = character.Id,
                Name = character.Name,
            }).ToList() 
        }).ToList();

        return Ok(allFaction);
    }

    [HttpPost]
    public IActionResult Create([FromBody] string name)
    {
        var faction = new Faction()
        {
            Name = name,
        };

        _context.Factions.Add(faction);
        _context.SaveChanges();

        return StatusCode(StatusCodes.Status201Created, faction);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var faction = _context.Factions.FirstOrDefault(f => f.Id == id);

        if(faction is null)
        {
            return NotFound();
        }

        _context.Factions.Remove(faction);
        _context.SaveChanges();

        return NoContent();
    }
}
