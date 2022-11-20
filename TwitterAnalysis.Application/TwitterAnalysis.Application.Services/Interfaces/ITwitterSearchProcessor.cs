using System.Threading.Tasks;
using TwitterAnalysis.Application.Messages.Request;
using TwitterAnalysis.Application.Messages.Response;

namespace TwitterAnalysis.Application.Services.Interfaces
{
    public interface ITwitterSearchProcessor
    {
        Task<TweetResponse> ProcessSearchByQuery(string query, PaginationQuery page);
    }
}
