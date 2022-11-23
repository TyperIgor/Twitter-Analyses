using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model;
using TwitterAnalysis.App.Service.Model.Data_Training;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Infrastructure.Data.Interfaces;

namespace TwitterAnalysis.App.Services.Data_Processor
{
    public class TrainingAlgorithmProcessor : ITrainingAlgorithmProcessor
    {
        private readonly ITweetRepository _tweetRepository;

        public TrainingAlgorithmProcessor(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }
        public async Task<DataTrainingResult> GetRacistsPhrasesForDataTraining()
        {
            var listPhrases = await _tweetRepository.GetRacistsPhrases();

            return MapperFromDatabase(listPhrases);
        }

        private static DataTrainingResult MapperFromDatabase(IEnumerable<RacistModelData> racistModelDatas)
        {
            return new DataTrainingResult()
            {
                DataList = racistModelDatas,
            };
        }
    }
}
