using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataContext.Data;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Add db context
        services.AddDbContext<BookDbContext>(options =>
            options.UseSqlite("Data Source=MyLocalDb.db", b => b.MigrationsAssembly("FunctionApp")));
    })
    .Build();

host.Run();
