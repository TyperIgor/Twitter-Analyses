using System;
using System.Collections.Generic;

namespace TwitterAnalysis.App.Service.Model
{
    public class TweetsResults
    {
        public IEnumerable<TweetData> Tweets { get; set; }

        public AlgorithmMetricsSummary AlgorithmMetricsSummary { get; set; }
    }
}
