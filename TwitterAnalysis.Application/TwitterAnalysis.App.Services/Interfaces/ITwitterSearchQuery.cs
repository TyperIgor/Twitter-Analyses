using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface ITwitterSearchQuery
    {
        Task<TweetsResults> GetTweetBySearch(string query);
    }
}
