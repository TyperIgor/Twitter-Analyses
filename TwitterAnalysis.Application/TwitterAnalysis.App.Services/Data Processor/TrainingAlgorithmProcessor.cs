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
        private readonly IDataTrainingRepository _tweetRepository;

        public TrainingAlgorithmProcessor(IDataTrainingRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }
        public async Task<DataTrainingResult> GetRacistsPhrasesForDataTraining()
        {
            var listPhrases = await _tweetRepository.GetRacistsPhrases();

            return MapperFromDomain(listPhrases);
        }

        private static DataTrainingResult MapperFromDomain(IEnumerable<RacistModelData> racistModelDatas)
        {
            return new DataTrainingResult()
            {
                DataList = racistModelDatas,
            };
        }

        public async Task InsertPhraseToAlgorithmTraining(RacistModelData data)
        {
            await _tweetRepository.InsertRacistPhraseAlgorithmTraining(data);
        }
    }
}
