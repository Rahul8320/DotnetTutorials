using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Data;
using VerticalSlice.Api.Endpoints;
using VerticalSlice.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options => options.CustomSchemaIds(t => t.FullName?.Replace('+', '.')));

builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddEndpoints();

builder.Services.AddHttpContextAccessor();

builder.Services.ServiceExtension();

builder.Services.AddEmailServerSetup(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
