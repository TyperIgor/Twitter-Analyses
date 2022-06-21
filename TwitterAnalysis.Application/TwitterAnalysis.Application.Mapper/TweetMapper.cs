using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.Application.Messages.Response;

namespace TwitterAnalysis.Application.Mapper
{
    public class TweetMapper
    {
        public static TweetResponse MapperTweetResponseModel(IEnumerable<TweetData> tweets)
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
