using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces
{
    public interface ITwitterServiceGateway
    {
        Task<IEnumerable<TweetData>> GetTweetBySearch(string query);
    }
}
