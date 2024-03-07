using Microsoft.Extensions.Options;

namespace AwesomeCompany.Options;

public class DatabaseOptionsSetup(IConfiguration configuration) : IConfigureOptions<DatabaseOptions>
{
    private const string ConfigurationSectionName = "DatabaseOptions";
    private readonly IConfiguration _configuration = configuration;

    public void Configure(DatabaseOptions options)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        options.ConnectionString = connectionString ?? throw new NullReferenceException();

        _configuration.GetSection(ConfigurationSectionName).Bind(options);

    }
}
