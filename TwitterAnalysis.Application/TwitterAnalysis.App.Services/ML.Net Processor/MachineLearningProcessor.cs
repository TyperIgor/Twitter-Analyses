using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Interfaces;

namespace TwitterAnalysis.App.Services.ML.Net_Processor
{
    public class MachineLearningProcessor : IMachineLearningProcessor
    {
        MLContext MlContext { get; set; } = new();
        private readonly IDataTrainingRepository _dataTrainingRepository;
        private readonly IGoogleSheetsApiProcessor _sheetsApi;

        public MachineLearningProcessor(IDataTrainingRepository dataTrainingRepository, 
                                        IGoogleSheetsApiProcessor sheetsApi)
        {
            _dataTrainingRepository = dataTrainingRepository;
            _sheetsApi = sheetsApi;
        }

        public async Task<TweetsResults> BuildBinaryAlgorithmClassificationToTweets(IList<TweetTextResponse> tweetDatas)
        {
            var modelsDatas = await _dataTrainingRepository.GetRacistsPhrases();

            var inputModel = ImplementAlgorithmTrainingFromCollection(modelsDatas);

            var trainingCollection = await _sheetsApi.ExtractSheetsContent();

            var dataViewFromModels = MlContext.Data.LoadFromEnumerable(trainingCollection);

            var predictions = inputModel.Transform(dataViewFromModels);

            var metrics = MlContext.BinaryClassification.Evaluate(predictions, "ActiveRacist");

            return GenerateAnalyseTextFromTweet(tweetDatas, inputModel, metrics);
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
