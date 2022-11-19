using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.App.Service.Common;

namespace TwitterAnalysis.Application.Mapper
{
    public class TweetMapper
    {
        public static TweetResponse MapperTweetResponseModel(TweetsResults tweets)
        {
            var tweetResponse = new TweetResponse {
                Data = tweets,
            };

            tweetResponse.StatusMessageTweetOperation(OperationMessageStatusEnum.SucessfullOperation);

            return tweetResponse;
        }
    }
}
