using System.Data;

namespace Dba.TestTask.Backend.Persistence.Factories.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}