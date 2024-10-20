using Microsoft.Extensions.DependencyInjection;
using PersonalManagementProject.Shared.Security.JWT;

namespace PersonalManagementProject.Shared.Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, JwtHelper>();
        return services;
    }
}