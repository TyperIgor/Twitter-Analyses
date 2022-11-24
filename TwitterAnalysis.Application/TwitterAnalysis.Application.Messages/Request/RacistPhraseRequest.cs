using System.Text.Json.Serialization;

namespace TwitterAnalysis.Application.Messages.Request
{
    public class RacistPhraseRequest
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("activeRacist")]
        public bool ActiveRacist { get; set; }
    }
}
