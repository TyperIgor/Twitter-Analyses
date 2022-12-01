using System.Text.Json.Serialization;

namespace TwitterAnalysis.Application.Messages.Request.Auth
{
    public class LoginAuthRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}