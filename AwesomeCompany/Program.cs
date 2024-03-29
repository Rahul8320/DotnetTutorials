using AwesomeCompany.Data;
using AwesomeCompany.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    (serviceProvider, dbContextOptionsBuilder) =>
    {
        var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

        dbContextOptionsBuilder.UseSqlite(databaseOptions.ConnectionString, sqlServerAction =>
        {
            sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
        });

        dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);

        dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
