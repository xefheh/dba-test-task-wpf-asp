using Dba.TestTask.Backend.Application.Services;
using Dba.TestTask.Backend.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dba.TestTask.Backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAbonentService, AbonentService>();
        return services;
    }
}