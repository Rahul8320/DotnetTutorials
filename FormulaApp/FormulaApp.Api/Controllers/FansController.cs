using FormulaApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FormulaApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FansController(IFanService fanService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var fans = await fanService.GetAllFans();

        if (fans == null || fans.Count == 0)
        {
            return NotFound();
        }

        return Ok(fans);
    }
}
