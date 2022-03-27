using System;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Application.Services.Interfaces;

namespace TwitterAnalysis.Application.Services
{
    public class TwitterSearchProcessor : ITwitterSearchProcessor
    {
        private readonly ITwitterSearchQuery _twitterSearchQuery;

        public TwitterSearchProcessor(ITwitterSearchQuery twitterSearchQuery)
        {
            _twitterSearchQuery = twitterSearchQuery;
        }


        public void ProcessSearch(string bearerToken)
        {
            _twitterSearchQuery.GetTweetBySearch("");
        }
    }
}
