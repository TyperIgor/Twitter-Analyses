using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Infrastructure.Service.Gateway.Interfaces;
using Microsoft.FeatureManagement;

namespace TwitterAnalysis.App.Services
{
    public class TwitterSearchService : ITwitterSearchQuery
    {
        private readonly ITwitterServiceGateway _twitterServiceGateway;
        private readonly IMachineLearningProcessor _machineLearningProcessor;
        private readonly IGoogleSheetsApiProcessor _fileSheets;
        private readonly IFeatureManager _manager;

        public TwitterSearchService(ITwitterServiceGateway twitterServiceGateway, 
                                    IMachineLearningProcessor machineLearningProcessor, 
                                    IGoogleSheetsApiProcessor fileSheets)
        {
            _twitterServiceGateway = twitterServiceGateway;
            _machineLearningProcessor = machineLearningProcessor;
            _fileSheets = fileSheets;   
        }

        public async Task<IEnumerable<TweetData>> GetTweetBySearch(string query)
        {
            var tweetData = await _twitterServiceGateway.GetTweetBySearch(query);

            return await _machineLearningProcessor.BuildBinaryAlgorithmClassificationToTweets(tweetData);
        }
    }
}
