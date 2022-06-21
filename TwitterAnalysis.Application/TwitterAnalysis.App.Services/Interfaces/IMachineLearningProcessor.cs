using System;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;
using Microsoft.ML;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface IMachineLearningProcessor
    {
        void BuildInputData(IEnumerable<RacistModelData> modelDatas , IEnumerable<TweetData> tweetDatas);
    }
}
