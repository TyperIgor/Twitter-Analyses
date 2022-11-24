using System;
using System.Threading.Tasks;
using TwitterAnalysis.Application.Messages.Response;

namespace TwitterAnalysis.Application.Services.Interfaces
{
    public interface ITrainingDataProcessor
    {
        Task<DataTrainingResponse> GetAlgorithmDataList();

        Task<DataTrainingResponse> InsertNewDataForTraining(object request);
    }
}
