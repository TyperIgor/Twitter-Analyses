using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;

namespace TwitterAnalysis.App.Services
{
    public class TwitterSearchService : ITwitterSearchQuery
    {
        private readonly ITwitterServiceGateway _twitterServiceGateway;
        private readonly IMachineLearningProcessor _machineLearningProcessor;

        public TwitterSearchService(ITwitterServiceGateway twitterServiceGateway, 
                                    IMachineLearningProcessor machineLearningProcessor)
        {
            _twitterServiceGateway = twitterServiceGateway;
            _machineLearningProcessor = machineLearningProcessor;  
        }

        public async Task<TweetsResults> GetTweetBySearch(string query)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var tweetData = await _twitterServiceGateway.GetTweetBySearch(query);

            var res = await _machineLearningProcessor.BuildBinaryAlgorithmClassificationToTweets(tweetData);
            stopWatch.Stop();

            Console.WriteLine($"Performance total: {stopWatch.Elapsed}, milliseconds: {stopWatch.ElapsedMilliseconds} ");

            return res;
        }
    }
}
