using FormulaApp.Api.Configurations;
using FormulaApp.Api.Services;
using FormulaApp.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddScoped<IFanService, FanService>();
builder.Services.AddHttpClient<IFanService, FanService>();
builder.Services.Configure<ApiServiceConfig>(builder.Configuration.GetSection("ApiServiceConfig"));

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