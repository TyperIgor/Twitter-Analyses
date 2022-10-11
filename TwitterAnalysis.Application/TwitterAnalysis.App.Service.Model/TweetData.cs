using Microsoft.ML.Data;
using System.Collections.Generic;

namespace TwitterAnalysis.App.Service.Model
{
    public class TweetData
    {
        public string TwitterUser { get; set; }

        [LoadColumnName("Label")]
        public string Text { get; set; }

        public bool TweetRacistResult { get; set; } = new bool();
    }
}
