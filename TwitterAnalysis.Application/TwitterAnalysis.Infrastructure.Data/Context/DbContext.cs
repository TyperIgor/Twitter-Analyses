using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using TwitterAnalysis.Infrastructure.Data.Interfaces;

namespace TwitterAnalysis.Infrastructure.Data.Context
{
    public class DbContext : IDbContext
    {
        private NpgsqlConnection _npgsqlConnection;
        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public NpgsqlConnection GetConnection()
        {
            if (_npgsqlConnection == null || _npgsqlConnection.State == ConnectionState.Closed)
                return CreateConnection();
            
            return _npgsqlConnection;
        }

        private NpgsqlConnection CreateConnection()
        {
            _npgsqlConnection = new NpgsqlConnection(_configuration.GetConnectionString("Database"));
            _npgsqlConnection.Open();
            return _npgsqlConnection;
        }
    }
}
