using Mapster;
using System.Threading.Tasks;
using TwitterAnalysis.Application.Mapper;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.Application.Services.Interfaces;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.Application.Services
{
    public class TrainingDataProcessor : ITrainingDataProcessor
    {
        private readonly ITrainingAlgorithmProcessor _trainingAlgorithmProcessor;
        public TrainingDataProcessor(ITrainingAlgorithmProcessor trainingAlgorithmProcessor)
        {
            _trainingAlgorithmProcessor = trainingAlgorithmProcessor;
        }

        public async Task<DataTrainingResponse> GetAlgorithmDataList()
        {
            var dataList = await _trainingAlgorithmProcessor.GetRacistsPhrasesForDataTraining();

            return DataAlgorithmMapper.MapperDataColletionTrainingToResponse(dataList);
        }

        public async Task<DataTrainingResponse> InsertNewDataForTraining(object request)
        {
            await _trainingAlgorithmProcessor.InsertPhraseToAlgorithmTraining(request.Adapt<RacistModelData>());

            return DataAlgorithmMapper.MapperTrainingResponseBaseOperation();
        }
    }
}
