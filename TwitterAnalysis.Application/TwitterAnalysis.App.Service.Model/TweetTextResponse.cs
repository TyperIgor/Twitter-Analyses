using Microsoft.ML.Data;

namespace TwitterAnalysis.App.Service.Model
{
    public class TweetTextResponse
    {
        public string User { get; set; }

        [LoadColumnName("Label")]
        public string Text { get; set; }
    }
}
