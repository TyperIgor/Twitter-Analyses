using Npgsql;

namespace TwitterAnalysis.Infrastructure.Data.Interfaces
{
    public interface IDbContext
    {
        NpgsqlConnection GetConnection();
    }
}
