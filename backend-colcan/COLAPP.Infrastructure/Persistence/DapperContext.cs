using System.Data;
using COLAPP.Shared.DependencyInjection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace COLAPP.Infrastructure.Persistence;

[Singleton]
public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("COLAPP") ?? "";
    }

    public IDbConnection CreateConection() => new SqlConnection(_connectionString);
}
