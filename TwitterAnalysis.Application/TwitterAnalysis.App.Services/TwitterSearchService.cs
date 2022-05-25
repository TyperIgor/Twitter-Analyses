using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters.V2;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.App.Services.Models;
using Tweetinvi.Models.V2;

namespace TwitterAnalysis.App.Services
{
    public class TwitterSearchService : ITwitterSearchQuery
    {
		private readonly IConfiguration _configuration;

        public TwitterSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		public async Task<IEnumerable<TweetData>> GetTweetBySearch(string query)
        {
            try
            {
                var appCredentials = Authenticate();

                var searchParameters = new SearchTweetsV2Parameters(query) { PageSize = 100, PlaceFields = new HashSet<string> { "Brasil"} };

                var response = await appCredentials.SearchV2.SearchTweetsAsync(searchParameters);

                return MapperTweetsResponse(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
                throw;
            }
        }

        private static IEnumerable<TweetData> MapperTweetsResponse(SearchTweetsV2Response responses)
        {
            var TweetData = new List<TweetData>();

            foreach (var response in responses.Tweets)
            {
                var tweet = new TweetData
                {
                    TwitterUser = response.Source,
                    Text = response.Text
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
