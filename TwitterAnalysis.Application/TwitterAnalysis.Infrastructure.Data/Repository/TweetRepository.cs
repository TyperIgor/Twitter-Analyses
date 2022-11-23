using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Query;

namespace TwitterAnalysis.Infrastructure.Data.Repository
{
    public class TweetRepository : Repository, ITweetRepository
    {
        public TweetRepository(IDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<RacistModelData>> GetRacistsPhrases()
        {
            try
            {
                return await _npgsqlConnection.QueryAsync<RacistModelData>(TweetQuery.TweetRacistModelQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
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
