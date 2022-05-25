using System.Threading.Tasks;
using TwitterAnalysis.Application.Messages.Response;

namespace TwitterAnalysis.Application.Services.Interfaces
{
    public interface ITwitterSearchProcessor
    {
        Task<TweetResponse> ProcessSearch(string query);
    }
}
