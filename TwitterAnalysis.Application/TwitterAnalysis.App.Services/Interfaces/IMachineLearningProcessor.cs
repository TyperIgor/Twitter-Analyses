using System;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;
using Microsoft.ML;
using System.Threading.Tasks;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface IMachineLearningProcessor
    {
        Task<IList<TweetData>> BuildBinaryAlgorithmClassificationToTweets(IList<TweetTextResponse> tweetDatas);
    }
}
