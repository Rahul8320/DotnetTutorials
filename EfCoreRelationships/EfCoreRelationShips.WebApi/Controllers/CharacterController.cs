using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationShips.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public ActionResult<List<Character>> GetAll()
    {
        var allCharacters = _context.Characters.Include(c => c.Backpack).ToList();

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

        return Created();
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

        return Ok();
    }
}
