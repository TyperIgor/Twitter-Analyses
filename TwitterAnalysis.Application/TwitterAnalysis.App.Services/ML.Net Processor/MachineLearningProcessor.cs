using Microsoft.ML;
using System.Collections.Generic;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Services.Interfaces;

namespace TwitterAnalysis.App.Services.ML.Net_Processor
{
    public class MachineLearningProcessor : IMachineLearningProcessor
    {
        public MLContext MlContext { get; set; } = new MLContext();

        public void BuildInputData(IEnumerable<RacistModelData> modelDatas, IEnumerable<TweetData> tweetDatas)
        {
            var dataview = MlContext.Data.LoadFromEnumerable(modelDatas);

            var pipeline = MlContext.Transforms.Text.FeaturizeText("Features", "Text")
                .Append(MlContext.BinaryClassification.Trainers.SdcaLogisticRegression(featureColumnName: "Features", labelColumnName: "ActiveRacist"));

            var model = pipeline.Fit(dataview);

            var dataViewAnalyse = MlContext.Data.LoadFromEnumerable(tweetDatas);
        }
    }
}
