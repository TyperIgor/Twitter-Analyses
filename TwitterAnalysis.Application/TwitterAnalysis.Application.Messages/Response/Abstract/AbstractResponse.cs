using System.Text.Json.Serialization;

namespace TwitterAnalysis.Application.Messages.Response.Abstract
{
    public abstract record AbstractResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}