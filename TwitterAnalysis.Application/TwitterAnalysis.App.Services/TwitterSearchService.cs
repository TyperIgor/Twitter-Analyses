using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Interfaces;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;

namespace TwitterAnalysis.App.Services
{
    public class TwitterSearchService : ITwitterSearchQuery
    {
        private readonly ITwitterServiceGateway _twitterServiceGateway;
        private readonly IMachineLearningProcessor _machineLearningProcessor;

        public TwitterSearchService(ITwitterServiceGateway twitterServiceGateway,IMachineLearningProcessor machineLearningProcessor)
        {
            _twitterServiceGateway = twitterServiceGateway;
            _machineLearningProcessor = machineLearningProcessor;
        }

        public async Task<IEnumerable<TweetData>> GetTweetBySearch(string query)
        {
            var tweetData = await _twitterServiceGateway.GetTweetBySearch(query);

            return await _machineLearningProcessor.BuildInputData(tweetData);
        }
    }
}
