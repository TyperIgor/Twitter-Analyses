using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAnalysis.Application.Mapper;
using TwitterAnalysis.App.Services.Interfaces;
using TwitterAnalysis.Application.Messages.Response;
using TwitterAnalysis.Application.Services.Interfaces;

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

            return DataAlgorithmMapper.MapperDataTrainingToResponse(dataList);
        }

        public Task InsertNewDataForTraining()
        {
            throw new NotImplementedException();
        }
    }
}
