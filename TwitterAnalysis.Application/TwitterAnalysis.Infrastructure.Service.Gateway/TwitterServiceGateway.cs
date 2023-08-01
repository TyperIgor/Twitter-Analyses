using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters;
using Tweetinvi.Parameters.V2;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Service.Model.Settings;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;

namespace TwitterAnalysis.Infrastructure.Service.Gateway
{
    public class TwitterServiceGateway : ITwitterServiceGateway
    {
        private readonly TwitterSettings _twitterSettings;
        private readonly ILogger<TwitterServiceGateway> _logger;

        public TwitterServiceGateway(IOptions<TwitterSettings> options, ILogger<TwitterServiceGateway> logger)
        {
            _twitterSettings = options.Value;
            _logger = logger;
        }

        public async Task<IList<TweetTextResponse>> GetTweetBySearch(string query, int pageSize)
        {
            try
            {
                ITwitterClient tweetClient = Authenticate();

                var searchParameters = BuildParameters(query, pageSize);

                var response = await tweetClient.SearchV2.SearchTweetsAsync(searchParameters);

                return MapperTweetsResponse(response.Tweets);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Error : {e.Message}");

                throw e;
            }
        }

        private static ISearchTweetsV2Parameters BuildParameters(string query, int pageSize)
        {
            return new SearchTweetsV2Parameters(query)
            {
                PageSize = pageSize,
                StartTime = DateTime.UtcNow.AddDays(-1),
                EndTime = DateTime.UtcNow
            };
        }

        #region private methods
        private static IList<TweetTextResponse> MapperTweetsResponse(TweetV2[] response)
        {
            var tweetV2Text = new List<TweetTextResponse>();

            foreach (var res in response)
            {
                var tweet = new TweetTextResponse
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
            return new TwitterClient(new ConsumerOnlyCredentials { BearerToken = _twitterSettings.BearerToken });
        }
        #endregion
    }
}
