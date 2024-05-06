using System.Data;
using Dba.TestTask.Backend.Persistence.Factories.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Dba.TestTask.Backend.Persistence.Factories;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;
    public DbConnectionFactory(IConfiguration configuration) => _configuration = configuration;

    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("DbConnection");
        if (connectionString == null) throw new NullReferenceException();
        return new SqliteConnection(connectionString);
    }
}