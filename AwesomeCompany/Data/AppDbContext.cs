using AwesomeCompany.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeCompany.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(builder =>
        {
            builder.ToTable("Companies");

            builder.HasMany(company => company.Employees).WithOne().HasForeignKey(employee => employee.CompanyId).IsRequired();

            builder.HasData(new Company
            {
                Id = 1,
                Name = "Awesome Company"
            });
        });

        modelBuilder.Entity<Employee>(builder =>
        {
            builder.ToTable("Employees");

            var employees = Enumerable.Range(1, 1000).Select(id => new Employee
            {
                Id = id,
                Name = $"Employee #{id}",
                Salary = 100.0m,
                CompanyId = 1,
            });

            builder.HasData(employees);
        });
    }
}
