using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces
{
    public interface ITwitterServiceGateway
    {
        Task<IList<TweetTextResponse>> GetTweetBySearch(string query, int pageSize);
    }
}
