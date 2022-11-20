using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Service.Model.Settings;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;

namespace TwitterAnalysis.Infrastructure.Service.Gateway
{
    public class TwitterServiceGateway : ITwitterServiceGateway
    {
        private readonly TwitterSettings _twitterSettings;

        public TwitterServiceGateway(IOptions<TwitterSettings> options)
        {
            _twitterSettings = options.Value;
        }

        public async Task<IList<TweetTextResponse>> GetTweetBySearch(string query, int pageSize)
        {
            try
            {
                ITwitterClient tweetClient = Authenticate();

                var searchParameters = BuildParameters(query, pageSize);

                var response = await tweetClient.Search.SearchTweetsAsync(searchParameters);

                return MapperTweetsResponse(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
                throw;
            }
        }

        private static SearchTweetsParameters BuildParameters(string query, int pageSize)
        {
            return new SearchTweetsParameters(query)
            {
                PageSize = pageSize,
                SearchType = SearchResultType.Recent,
                ContinueMinMaxCursor = ContinueMinMaxCursor.UntilNoItemsReturned,
            };
        }

        #region private methods
        private static IList<TweetTextResponse> MapperTweetsResponse(ITweet[] response)
        {
            var tweetV2Text = new List<TweetTextResponse>();

            foreach (var res in response)
            {
                var tweet = new TweetTextResponse
                {
                    User = res.CreatedBy.Name,
                    Text = res.Text
                };

                tweetV2Text.Add(tweet);
            }

            return tweetV2Text;
        }

        private TwitterClient Authenticate()
        {
            return new TwitterClient(new ConsumerOnlyCredentials { BearerToken = _twitterSettings.BearerToken });
        }
        #endregion
    }
}
