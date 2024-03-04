using AwesomeCompany.Data;
using AwesomeCompany.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeCompany.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    [Route("{companyId:int}")]
    public async Task<IActionResult> Get(int companyId)
    {
        var companyDetails = await _context.Set<Company>().Include(c => c.Employees).FirstOrDefaultAsync(c => c.Id == companyId);

        if (companyDetails == null)
        {
            return NotFound();
        }

        return Ok(companyDetails);
    }

    [HttpPut]
    [Route("increase-salaries/{companyId:int}")]
    public async Task<IActionResult> UpdateSalaries(int companyId)
    {
        var companyDetails = await _context.Set<Company>().Include(c => c.Employees).FirstOrDefaultAsync(c => c.Id == companyId);

        if (companyDetails == null)
        {
            return NotFound();
        }

        foreach (var employee in companyDetails.Employees)
        {
            employee.Salary *= 1.1m;
        }

        companyDetails.LastSalaryUpdateUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
