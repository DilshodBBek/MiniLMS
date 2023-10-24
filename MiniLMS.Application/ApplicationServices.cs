using Microsoft.Extensions.DependencyInjection;
using MiniLMS.Application.Mappings;
using MiniLMS.Application.Services;
using System.Reflection;

namespace MiniLMS.Application;
public static class ApplicationServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(
            new List<Assembly>() { Assembly.GetAssembly(typeof(MiniLMS.Application.Mappings.MappingProfile)) },
            ServiceLifetime.Singleton);

        services.AddTransient<ITestService, TestService>();
    }
}
