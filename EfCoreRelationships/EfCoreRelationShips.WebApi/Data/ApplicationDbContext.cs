using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationShips.WebApi;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
}
