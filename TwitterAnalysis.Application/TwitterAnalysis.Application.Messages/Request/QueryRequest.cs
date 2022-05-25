using System.Text.Json.Serialization;

namespace TwitterAnalysis.Application.Messages.Request
{
    public class QueryRequest
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }
    }
}
