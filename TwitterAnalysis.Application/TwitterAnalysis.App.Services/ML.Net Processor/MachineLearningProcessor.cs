using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Infrastructure.Cache.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Interfaces;

namespace TwitterAnalysis.App.Services.ML.Net_Processor
{
    public class MachineLearningProcessor : IMachineLearningProcessor
    {
        MLContext MlContext { get; set; } = new();
        private readonly IDataTrainingRepository _dataTrainingRepository;
        private readonly IGoogleSheetsApiProcessor _sheetsApi;
        private readonly ICacheService _cacheService;

        public MachineLearningProcessor(IDataTrainingRepository dataTrainingRepository, 
                                        IGoogleSheetsApiProcessor sheetsApi,
                                        ICacheService cacheService)
        {
            _dataTrainingRepository = dataTrainingRepository;
            _sheetsApi = sheetsApi;
            _cacheService = cacheService;
        }

        public async Task<TweetsResults> BuildBinaryAlgorithmClassificationToTweets(IList<TweetTextResponse> tweetDatas)
        {
            var modelsDatas = await TryGetModelTrainingFromCache();

            var inputModel = ImplementAlgorithmTrainingFromCollection(modelsDatas);

            var trainingCollection = await _sheetsApi.ExtractSheetsContent();

            var dataViewFromModels = MlContext.Data.LoadFromEnumerable(trainingCollection);

            var predictions = inputModel.Transform(dataViewFromModels);

            var metrics = MlContext.BinaryClassification.Evaluate(predictions, "ActiveRacist");

            return GenerateAnalyseTextFromTweet(tweetDatas, inputModel, metrics);
        }

        private async Task<IEnumerable<RacistModelData>> TryGetModelTrainingFromCache()
        {
            var dataTrainingCache = await _cacheService.GetAllDataTrainingInCache("RacistModelList");

            if (dataTrainingCache != null)
                return JsonSerializer.Deserialize<IEnumerable<RacistModelData>>(dataTrainingCache);

            var modelTraining = await _dataTrainingRepository.GetRacistsPhrases();

            await _cacheService.SetCacheDataAsync("RacistModelList", modelTraining);

            return modelTraining;
        }

        #region private methods
        private ITransformer ImplementAlgorithmTrainingFromCollection(IEnumerable<RacistModelData> racistModels)
        {
            var dataview = MlContext.Data.LoadFromEnumerable(racistModels);

            var pipeline = MlContext.Transforms.Text.FeaturizeText("Features", "Text")
                                                    .AppendCacheCheckpoint(MlContext)
                                                    .Append(MlContext.BinaryClassification.Trainers
                                                    .SdcaLogisticRegression(featureColumnName: "Features", labelColumnName: "ActiveRacist"));

            return pipeline.Fit(dataview);
        }

        private TweetsResults GenerateAnalyseTextFromTweet(IList<TweetTextResponse> tweets, ITransformer model, BinaryClassificationMetrics metrics)
        {
            var predictEngine = MlContext.Model.CreatePredictionEngine<RacistModelData, TweetClassification>(model);

            var modelData = new RacistModelData();
            var tweetResult = new TweetsResults();

            foreach (var twt in tweets)
            {
                modelData.Text = twt.Text;

                var feedback = predictEngine.Predict(modelData);

                tweetResult.Tweets.Add(new TweetData()
                {
                    User = twt.User,
                    Text = twt.Text,
                    TweetRacistResult = feedback.WasRacist
                });
            }

            tweetResult.AlgorithmMetricsSummary = new()
            {
                Accuracy = metrics.Accuracy,
                PositivePrecision = metrics.PositivePrecision,
                NegativePrecision = metrics.NegativePrecision,
                F1Score = metrics.F1Score,
                AreaUnderCurve = metrics.AreaUnderRocCurve
            };

            return tweetResult;
        }

        #endregion
    }
}
