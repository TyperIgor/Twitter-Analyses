using System.Collections.Generic;
using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.App.Services.Models;

namespace TwitterAnalysis.Application.Mapper
{
    public class TweetMapper
    {
        public static TweetResponse MapperTweetModel(IEnumerable<TweetData> tweets)
        {
            var tweetResponse = new TweetResponse();
            var tweetdataList = new List<TweetData>();

            foreach (var tweet in tweets)
            {
                tweetdataList.Add(new TweetData()
                {
                    TwitterUser = tweet.TwitterUser,
                    Text = tweet.Text,
                });
            }

            tweetResponse.Data = tweetdataList;
            tweetResponse.Message = "completed";

            return tweetResponse;

        }
    }
}
