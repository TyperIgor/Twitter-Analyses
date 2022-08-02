using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Query;

namespace TwitterAnalysis.Infrastructure.Data.Repository
{
    public class TweetRepository : Repository, ITweetRepository
    {
        public TweetRepository(IDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<RacistModelData>> GetRacistsPhrasesToModelEnter() => await _npgsqlConnection.QueryAsync<RacistModelData>(TweetQuery.TweetRacistModelQuery);
    }
}
