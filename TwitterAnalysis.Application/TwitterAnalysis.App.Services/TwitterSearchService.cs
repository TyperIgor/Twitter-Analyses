using System.Linq;
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

        public async Task<TweetsResults> GetTweetBySearch(string query, int pageSize)
        {
            var tweetData = await _twitterServiceGateway.GetTweetBySearch(query, pageSize);

            if (!tweetData.Any())
                return null;

            return await _machineLearningProcessor.BuildBinaryAlgorithmClassificationToTweets(tweetData);
        }

        public Task GetUserTweet(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
