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

        public async Task<IList<TweetV2TextResponse>> GetTweetBySearch(string query)
        {
            try
            {
                ITwitterClient tweetClient = Authenticate();

                var searchParameters = new SearchTweetsV2Parameters(query) { PageSize = 10 };

                var response = await tweetClient.SearchV2.SearchTweetsAsync(searchParameters);

                return MapperTweetsResponse(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
                throw;
            }
        }

        private static IList<TweetV2TextResponse> MapperTweetsResponse(SearchTweetsV2Response response)
        {
            var tweetV2Text = new List<TweetV2TextResponse>();

            foreach (var res in response.Tweets)
            {
                var tweet = new TweetV2TextResponse
                {
                    User = res.AuthorId,
                    Text = res.Text
                };

                tweetV2Text.Add(tweet);
            }

            return tweetV2Text;
        }

        private TwitterClient Authenticate()
        {
            return new TwitterClient(new ConsumerOnlyCredentials { BearerToken = _configuration.GetSection("BearerToken").Value });
        }
    }
}
