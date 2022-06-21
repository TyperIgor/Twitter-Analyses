using System.Collections.Generic;
using System.Text.Json.Serialization;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.Application.Messages.Response
{
    public class TweetResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public IEnumerable<TweetData> Data { get; set; }
    }
}
