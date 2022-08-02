using Microsoft.ML.Data;

namespace TwitterAnalysis.App.Service.Model
{
    public class TweetV2TextResponse
    {
        public string User { get; set; }

        [LoadColumnName("Label")]
        public string Text { get; set; }
    }
}
