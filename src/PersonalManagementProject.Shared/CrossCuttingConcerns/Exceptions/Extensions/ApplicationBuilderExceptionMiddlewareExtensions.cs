using Microsoft.AspNetCore.Builder;
using PersonalManagementProject.Shared.CrossCuttingConcerns.Exceptions.Middleware;

namespace PersonalManagementProject.Shared.CrossCuttingConcerns.Exceptions.Extensions;

public static class ApplicationBuilderExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
