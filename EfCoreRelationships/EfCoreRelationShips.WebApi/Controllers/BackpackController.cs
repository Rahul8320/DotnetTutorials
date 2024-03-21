using EfCoreRelationShips.WebApi.Model.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationShips.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BackpackController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public ActionResult<List<Backpack>> GetAll()
    {
        var allBackpacks = _context.Backpacks.Select(item => new {
            item.Id,
            item.Description,
            CharacterId = item.Character.Id,
            CharacterName = item.Character.Name,
        }).ToList();

        return Ok(allBackpacks);
    }

    [HttpPost]
    public IActionResult Create([FromBody] AddBackpackDto model)
    {
        var backpack = new Backpack()
        {
            Description = model.Description,
            CharacterId = model.CharacterId,
        };

        _context.Backpacks.Add(backpack);
        _context.SaveChanges();

        return Created();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var backpack = _context.Backpacks.FirstOrDefault(b => b.Id == id);

        if(backpack is null)
        {
            return NotFound();
        }

        _context.Backpacks.Remove(backpack);
        _context.SaveChanges();

        return Ok();
    }
}
