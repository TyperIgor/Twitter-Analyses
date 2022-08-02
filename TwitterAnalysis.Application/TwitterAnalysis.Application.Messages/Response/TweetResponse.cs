using System.Collections.Generic;
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
        public IEnumerable<TweetData> Data { get; set; }

        public TweetResponse StatusMessageTweetOperation(OperationMessageStatusEnum statusEnum)
        {
            Message = statusEnum.GetDescription();
            return this;
        }
    }
}
