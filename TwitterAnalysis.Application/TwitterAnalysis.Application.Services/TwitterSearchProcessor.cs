using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Application.Mapper;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.Application.Messages.Response;

namespace TwitterAnalysis.Application.Services
{
    public class TwitterSearchProcessor : ITwitterSearchProcessor
    {
        private readonly ITwitterSearchQuery _twitterSearchQuery;

        public TwitterSearchProcessor(ITwitterSearchQuery twitterSearchQuery)
        {
            _twitterSearchQuery = twitterSearchQuery;
        }

        public async Task<TweetResponse> ProcessSearch(string query)
        {
            try
            {
                var tweet = await _twitterSearchQuery.GetTweetBySearch(query);

                return TweetMapper.MapperTweetModel(tweet);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
                throw e;
            }
        }
    }
}
