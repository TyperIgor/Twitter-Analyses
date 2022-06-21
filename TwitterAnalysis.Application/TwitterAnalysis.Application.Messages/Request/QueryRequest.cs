using System.Text.Json.Serialization;

namespace TwitterAnalysis.Application.Messages.Request
{
    public class QueryRequest
    {
        /// <summary>
        /// Field to search a content on Twitter
        /// </summary>
        [JsonPropertyName("query")]
        public string Query { get; set; }
    }
}
