using System.Text.Json.Serialization;
using TwitterAnalysis.App.Service.Common;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Service.Common.Extension;

namespace TwitterAnalysis.Application.Messages.Response
{
    public record TweetResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public TweetsResults Data { get; set; }

        public TweetResponse StatusMessageTweetOperation(OperationMessageStatusEnum statusEnum)
        {
            Message = statusEnum.GetDescription();
            return this;
        }
    }
}
