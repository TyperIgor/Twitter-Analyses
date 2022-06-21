using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;
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

        public async Task<IEnumerable<TweetData>> GetTweetBySearch(string query)
        {
            try
            {
                ITwitterClient tweetClient = Authenticate();

                var searchParameters = new SearchTweetsV2Parameters(query) { PageSize = 100 };

                var response = await tweetClient.SearchV2.SearchTweetsAsync(searchParameters);

                return MapperTweetsResponse(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
                throw;
            }
        }

        private static IEnumerable<TweetData> MapperTweetsResponse(SearchTweetsV2Response response)
        {
            var TweetData = new List<TweetData>();

            foreach (var res in response.Tweets)
            {
                var tweet = new TweetData
                {
                    TwitterUser = res.AuthorId,
                    Text = res.Text
                };

                TweetData.Add(tweet);
            }

            return TweetData;
        }

        private TwitterClient Authenticate()
        {
            return new TwitterClient(new ConsumerOnlyCredentials { BearerToken = _configuration.GetSection("BearerToken").Value });
        }
    }
}
