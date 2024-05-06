using System.Data;
using Dba.TestTask.Backend.Application.Persistence.Interfaces;
using Dba.TestTask.Backend.Persistence.Factories;
using Dba.TestTask.Backend.Persistence.Factories.Interfaces;
using Dba.TestTask.Backend.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;

namespace Dba.TestTask.Backend.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        Batteries.Init();
        
        services.AddSingleton<IDbConnectionFactory>(_ =>
        {
            var factory = new DbConnectionFactory(configuration);
            return factory;
        });

        services.AddScoped<IAbonentRepository, AbonentRepository>();

        return services;
    }
}