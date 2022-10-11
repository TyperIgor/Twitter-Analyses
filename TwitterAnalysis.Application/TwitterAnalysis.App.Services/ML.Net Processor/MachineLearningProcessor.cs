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
        MLContext MlContext { get; set; } = new MLContext();
        private readonly ITweetRepository _tweetRepository;
        private readonly IGoogleSheetsApiProcessor _sheetsApi;

        public MachineLearningProcessor(ITweetRepository tweetRepository, IGoogleSheetsApiProcessor sheetsApi)
        {
            _tweetRepository = tweetRepository;
            _sheetsApi = sheetsApi;
        }

        public async Task<TweetsResults> BuildBinaryAlgorithmClassificationToTweets(IList<TweetTextResponse> tweetDatas)
        {
            var trainingCollection = await _sheetsApi.ExtractSheetsContent();

            var inputModel = ImplementAlgorithmTrainingFromCollection(trainingCollection);

            var modelsDatas = await _tweetRepository.GetRacistsPhrasesToModelEnter();

            var dataViewFromModels = MlContext.Data.LoadFromEnumerable(modelsDatas);

            var predictions = inputModel.Transform(dataViewFromModels);

            var metrics = MlContext.BinaryClassification.Evaluate(predictions, "ActiveRacist");

            return GenerateAnalyseTextFromTweet(tweetDatas, inputModel, MlContext, metrics);
        }

        #region private methods
        private ITransformer ImplementAlgorithmTrainingFromCollection(IEnumerable<RacistModelData> racistModels)
        {
            var dataview = MlContext.Data.LoadFromEnumerable(racistModels);

            var pipeline = MlContext.Transforms.Text.FeaturizeText("Features", "Text")
                .AppendCacheCheckpoint(MlContext)
                .Append(MlContext.BinaryClassification.Trainers.SdcaLogisticRegression(featureColumnName: "Features", labelColumnName: "ActiveRacist"));

            return pipeline.Fit(dataview);
        }

        private static TweetsResults GenerateAnalyseTextFromTweet(IList<TweetTextResponse> tweets, ITransformer model, MLContext mLContext, BinaryClassificationMetrics metrics)
        {
            PredictionEngine<RacistModelData, TweetClassification> predictEngine = mLContext.Model.CreatePredictionEngine<RacistModelData, TweetClassification>(model);

            var modelData = new RacistModelData();
            var tweetResult = new TweetsResults();

            foreach (var twt in tweets)
            {
                modelData.Text = twt.Text;

                var feedback = predictEngine.Predict(modelData);

                tweetResult.Tweets.Add(new TweetData()
                {
                    TwitterUser = twt.User,
                    Text = twt.Text,
                    TweetRacistResult = feedback.WasRacist
                });
            }

            tweetResult.AlgorithmMetricsSummary = new AlgorithmMetricsSummary()
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
