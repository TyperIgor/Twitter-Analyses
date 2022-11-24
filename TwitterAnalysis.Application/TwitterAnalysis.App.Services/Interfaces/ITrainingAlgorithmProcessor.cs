using System.Threading.Tasks;
using TwitterAnalysis.App.Service.Model.Data_Training;
using TwitterAnalysis.App.Service.Model;

namespace TwitterAnalysis.App.Services.Interfaces
{
    public interface ITrainingAlgorithmProcessor
    {
        Task<DataTrainingResult> GetRacistsPhrasesForDataTraining();

        Task InsertPhraseToAlgorithmTraining(RacistModelData data);

    }
}
