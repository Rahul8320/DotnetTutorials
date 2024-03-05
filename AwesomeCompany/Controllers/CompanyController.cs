using AwesomeCompany.Data;
using AwesomeCompany.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
        // Entity framework core create 1000 sql update command to perform this operation 🙀🙀
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

        await _context.SaveChangesAsync(); // 1 more sql command -> total 1001 sql commands 🙀🙀

        return NoContent();
    }

    [HttpPut]
    [Route("increase-salaries-sql/{companyId:int}")]
    public async Task<IActionResult> UpdateSalariesSql(int companyId)
    {
        var companyDetails = await _context.Set<Company>().Include(c => c.Employees).FirstOrDefaultAsync(c => c.Id == companyId);

        if (companyDetails == null)
        {
            return NotFound();
        }

        // begin database transaction
        await _context.Database.BeginTransactionAsync();

        // performing row sql query 
        await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Employees SET Salary = Salary * 1.1 WHERE CompanyId = {companyDetails.Id}");

        companyDetails.LastSalaryUpdateUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // commit database transaction
        await _context.Database.CommitTransactionAsync();

        return NoContent();
    }

    [HttpPut]
    [Route("increase-salaries-dapper/{companyId:int}")]
    public async Task<IActionResult> UpdateSalariesDapper(int companyId)
    {
        var companyDetails = await _context.Set<Company>().Include(c => c.Employees).FirstOrDefaultAsync(c => c.Id == companyId);

        if (companyDetails == null)
        {
            return NotFound();
        }

        // begin database transaction
        var transaction = await _context.Database.BeginTransactionAsync();

        // performing sql query using dapper
        await _context.Database.GetDbConnection().ExecuteAsync(
            "UPDATE Employees SET Salary = Salary * 1.1 WHERE CompanyId = @CompanyId",
            new { CompanyId = companyDetails.Id },
            transaction.GetDbTransaction()
        );

        companyDetails.LastSalaryUpdateUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // commit database transaction
        await _context.Database.CommitTransactionAsync();

        return NoContent();
    }
}
