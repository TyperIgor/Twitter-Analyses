using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Services.Models;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface ITwitterSearchQuery
    {
        Task<IEnumerable<TweetData>> GetTweetBySearch(string query);
    }
}
