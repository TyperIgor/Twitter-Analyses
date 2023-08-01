using Dapper;
using System;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Query;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Logging;

namespace TwitterAnalysis.Infrastructure.Data.Repository
{
    public class LoginRepository : Repository, ILoginRepository
    {
        private readonly ILogger<LoginRepository> _logger;
        public LoginRepository(IDbContext dbContext, ILogger<LoginRepository> logger) : base(dbContext) 
        {
            _logger = logger;
        }

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
            catch (Exception e)
            {
                _logger.LogError("Error:", e.Message);
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
