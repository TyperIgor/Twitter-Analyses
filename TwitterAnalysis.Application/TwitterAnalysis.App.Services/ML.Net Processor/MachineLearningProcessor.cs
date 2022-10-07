using Microsoft.ML;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Interfaces;

namespace TwitterAnalysis.App.Services.ML.Net_Processor
{
    public class MachineLearningProcessor : IMachineLearningProcessor
    {
        public MLContext MlContext { get; set; } = new MLContext();
        private readonly ITweetRepository _tweetRepository;
        private readonly IGoogleSheetsApiProcessor _fileSheets;

        public MachineLearningProcessor(ITweetRepository tweetRepository, IGoogleSheetsApiProcessor fileSheets)
        {
            _tweetRepository = tweetRepository;
            _fileSheets = fileSheets;
        }

        public async Task<IList<TweetData>> BuildBinaryAlgorithmClassificationToTweets(IList<TweetTextResponse> tweetDatas)
        {
            var modelsDatas = await _tweetRepository.GetRacistsPhrasesToModelEnter();

            var inputModel = ImplementAlgorithmTrainingFromCollection(modelsDatas);

            var trainingCollection = await _fileSheets.ExtractSheetsContent();

            var dataview1 = MlContext.Data.LoadFromEnumerable(trainingCollection);

            var predictions = inputModel.Transform(dataview1);

            var metrics = MlContext.BinaryClassification.Evaluate(predictions, "ActiveRacist");

            return GenerateAnalyseTextFromTweet(tweetDatas, inputModel, MlContext);
        }

        #region private methods
        private ITransformer ImplementAlgorithmTrainingFromCollection(IEnumerable<RacistModelData> racistModels)
        {
            var dataview = MlContext.Data.LoadFromEnumerable(racistModels);

            var pipeline = MlContext.Transforms.Text.FeaturizeText("Features", "Text")
                .Append(MlContext.BinaryClassification.Trainers.SdcaLogisticRegression(featureColumnName: "Features", labelColumnName: "ActiveRacist"));

            return pipeline.Fit(dataview);
        }

        private static IList<TweetData> GenerateAnalyseTextFromTweet(IList<TweetTextResponse> tweets, ITransformer model, MLContext mLContext)
        {
            var predictEngine = mLContext.Model.CreatePredictionEngine<RacistModelData, TweetClassification>(model);

            var modelData = new RacistModelData();
            var tweetData = new List<TweetData>();

            foreach (var twt in tweets)
            {
                modelData.Text = twt.Text;

                var feedback = predictEngine.Predict(modelData);

                tweetData.Add(new TweetData()
                {
                    TwitterUser = twt.User,
                    Text = twt.Text,
                    TweetRacistResult = feedback.WasRacist
                });
            }

            return tweetData;
        }
        #endregion
    }
}
