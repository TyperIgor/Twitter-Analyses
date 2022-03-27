using System;
using System.Threading.Tasks;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface ITwitterSearchQuery
    {
        void GetTweetBySearch(string query);

    }
}
