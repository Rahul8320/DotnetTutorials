using EfCoreRelationShips.WebApi.Model;
using EfCoreRelationShips.WebApi.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreRelationShips.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeaponController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public ActionResult<List<WeaponDto>> GetAll()
    {
        var allWeapons = _context.Weapons.Select(item => new WeaponDto(){
            Id = item.Id,
            Name = item.Name,
            CharacterId = item.Character.Id,
            CharacterName = item.Character.Name,
        }).ToList();

        return Ok(allWeapons);
    }

    [HttpPost]
    public IActionResult Create([FromBody] AddWeaponDto model)
    {
        var weapon = new Weapon()
        {
            Name = model.Name,
            CharacterId = model.CharacterId,
        };

        _context.Weapons.Add(weapon);
        _context.SaveChanges();

        return StatusCode(StatusCodes.Status201Created, weapon);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var weapon = _context.Weapons.FirstOrDefault(w => w.Id == id);

        if(weapon is null)
        {
            return NotFound();
        }

        _context.Weapons.Remove(weapon);
        _context.SaveChanges();

        return NoContent();
    }
}
