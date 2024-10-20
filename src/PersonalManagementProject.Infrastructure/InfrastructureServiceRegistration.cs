using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Infrastructure.Persistence.Contexts;
using PersonalManagementProject.Infrastructure.Persistence.Interceptors;
using PersonalManagementProject.Infrastructure.Persistence.Repositories;
using PersonalManagementProject.Infrastructure.Persistence.Repositories.Base;
using PersonalManagementProject.Shared.Persistence.Abstraction;

namespace PersonalManagementProject.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, EntitySaveChangesInterceptor>();
        var cs = configuration.GetConnectionString("Default");
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(cs);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repository konfigürasyonlarý
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<ILeaveRepository, LeaveRepository>();
        services.AddScoped<ISalaryPaymentRepository, SalaryPaymentRepository>();
        services.AddScoped<IPerformanceReviewRepository, PerformanceReviewRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IEmployeeRoleRepository, EmployeeRoleRepository>();
        services.AddScoped<IEmployeePermissionRepository, EmployeePermissionRepository>();
        return services;
    }
}