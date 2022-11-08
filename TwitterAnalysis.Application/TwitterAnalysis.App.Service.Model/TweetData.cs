using Microsoft.ML.Data;

namespace TwitterAnalysis.App.Service.Model
{
    public class TweetData
    {
        public string User { get; set; }

        [LoadColumnName("Label")]
        public string Text { get; set; }

        public bool TweetRacistResult { get; set; } = new bool();
    }
}
