using Npgsql;
using System.Threading.Tasks;
using TwitterAnalysis.Infrastructure.Data.Interfaces;

namespace TwitterAnalysis.Infrastructure.Data.Repository
{
    public abstract class Repository
    {
        protected readonly NpgsqlConnection _npgsqlConnection;

        public Repository(IDbContext dbContext) => _npgsqlConnection = dbContext.GetConnection();

        public void Dispose()
        {
            _npgsqlConnection.Dispose();
        }

        public async Task CloseConnection()
        {
            await _npgsqlConnection.CloseAsync();
        }
    }
}
