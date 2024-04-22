using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataContext.Data;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();


        // Add db context
        services.AddDbContext<BookDbContext>(options =>
            options.UseSqlite(configuration["DbConnection"], b => b.MigrationsAssembly("FunctionApp")));
    })
    .Build();

host.Run();
