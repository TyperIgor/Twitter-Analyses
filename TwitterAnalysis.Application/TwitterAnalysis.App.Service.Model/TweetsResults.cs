using System;
using System.Collections.Generic;

namespace TwitterAnalysis.App.Service.Model
{
    public class TweetsResults
    {
        public TweetsResults()
        {
            AlgorithmMetricsSummary = new AlgorithmMetricsSummary();

            Tweets = new List<TweetData>();
        }

        public List<TweetData> Tweets { get; set; } 

        public AlgorithmMetricsSummary AlgorithmMetricsSummary { get; set; } 
    }
}
