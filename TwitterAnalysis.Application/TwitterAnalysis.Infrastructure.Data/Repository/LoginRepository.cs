using Dapper;
using System;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Query;
using System.Threading.Tasks;
using System.Data;

namespace TwitterAnalysis.Infrastructure.Data.Repository
{
    public class LoginRepository : Repository, ILoginRepository
    {
        public LoginRepository(IDbContext dbContext) : base(dbContext) { }

        public async Task<bool> CheckLogin(string username, string password)
        {
            try
            {
                var parameters = new
                {
                    Name = username,
                    Secret = password,
                };

                return await _npgsqlConnection.ExecuteScalarAsync<bool>(LoginQuery.UserQuery, parameters);
            }
            catch
            {
                throw;
            }
            finally
            {
                await CloseConnection();

                if (_npgsqlConnection.State == ConnectionState.Closed)
                    Dispose();
            }
        }
    }
}
