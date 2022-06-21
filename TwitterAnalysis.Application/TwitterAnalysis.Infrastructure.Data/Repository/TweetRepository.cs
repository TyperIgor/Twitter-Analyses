using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.Infrastructure.Data.Interfaces;

namespace TwitterAnalysis.Infrastructure.Data.Repository
{
    public class TweetRepository : Repository, ITweetRepository
    {
        public TweetRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<RacistModelData>> GetRacistsPhrasesToModelEnter()
        {
            var result = await _npgsqlConnection.QueryAsync<RacistModelData>("select * from \"RacistComment\" ");

            return result;
        }
    }
}
