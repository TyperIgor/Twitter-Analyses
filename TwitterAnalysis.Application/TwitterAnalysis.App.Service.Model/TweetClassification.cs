using Microsoft.ML.Data;

namespace TwitterAnalysis.App.Service.Model
{
    public class TweetClassification : RacistModelData
    {
        [ColumnName("PredictedLabel")]
        public bool WasRacist { get; set; }
    }
}
