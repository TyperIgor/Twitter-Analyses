using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;

namespace TwitterAnalysis.Infrastructure.Service.Gateway
{
    public class TwitterServiceGateway : ITwitterServiceGateway
    {
        private readonly IConfiguration _configuration;

        public TwitterServiceGateway(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public async Task<IList<TweetTextResponse>> GetTweetBySearch(string query)
        {
            try
            {
                ITwitterClient tweetClient = Authenticate();

                var searchParameters = new SearchTweetsParameters(query) { PageSize = 10, SearchType = SearchResultType.Recent };

                var response = await tweetClient.Search.SearchTweetsAsync(searchParameters);

                return MapperTweetsResponse(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
                throw;
            }
        }


        #region private methods
        private static IList<TweetTextResponse> MapperTweetsResponse(ITweet[] response)
        {
            var stopwatch = new Stopwatch();
            var tweetV2Text = new List<TweetTextResponse>();

            stopwatch.Start();
            foreach (var res in response)
            {
                var tweet = new TweetTextResponse
                {
                    User = res.CreatedBy.Name,
                    Text = res.Text
                };

                tweetV2Text.Add(tweet);
            }
            stopwatch.Stop();

            Console.WriteLine($"Performance {stopwatch.Elapsed} , milliseconds {stopwatch.ElapsedMilliseconds}");

            return tweetV2Text;
        }

        private TwitterClient Authenticate()
        {
            return new TwitterClient(new ConsumerOnlyCredentials { BearerToken = _configuration.GetSection("BearerToken").Value });
        }
        #endregion
    }
}
