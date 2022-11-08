using System.Text.Json.Serialization;

namespace TwitterAnalysis.App.Service.Model.Settings
{
    public class TwitterSettings
    {
        [JsonPropertyName("BearerToken")]
        public string BearerToken { get; set; }
    }
}
