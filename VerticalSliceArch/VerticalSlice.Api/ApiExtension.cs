using VerticalSlice.Api.Users;

namespace VerticalSlice.Api;

public static class ApiExtension
{
    public static IServiceCollection ServiceExtension(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<RegisterUser>();
        services.AddScoped<LoginUser>();

        return services;
    }

    public static IServiceCollection AddEmailServerSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddFluentEmail(configuration["Email:SenderEmail"], configuration["Email:Sender"])
            .AddSmtpSender(configuration["Email:Host"], configuration.GetValue<int>("Email:Port"));

        return services;
    }
}
