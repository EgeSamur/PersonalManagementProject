using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalManagementProject.Application.Features.Auth.Auth;
using PersonalManagementProject.Application.Features.Auth.Permissions;
using PersonalManagementProject.Application.Features.Auth.Roles;
using PersonalManagementProject.Application.Features.Employees;
using PersonalManagementProject.Shared.CrossCuttingConcerns.Logging.Serilog;
using PersonalManagementProject.Shared.CrossCuttingConcerns.Logging.Serilog.Logger;
using SnifferApi.Application.Abstractions.Services;
using System.Reflection;

namespace PersonalManagementProject.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSingleton<LoggerServiceBase, FileLogger>();


        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPermissionService, PermissionService>();

        return services;
    }
}
