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
}
